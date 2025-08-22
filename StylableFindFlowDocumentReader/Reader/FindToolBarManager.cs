using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using StylableFindFlowDocumentReader.Utils;

namespace StylableFindFlowDocumentReader.Reader
{
    public class FindToolBarManager
    {
        internal static class DocumentViewHelper
        {
            private static readonly MethodInfo s_keyDownHelperMethod;

            static DocumentViewHelper()
            {
                Type documentViewHelperType = typeof(FlowDocumentPageViewer).Assembly.GetType("MS.Internal.Documents.DocumentViewerHelper");
                s_keyDownHelperMethod = documentViewHelperType.GetMethod("KeyDownHelper", BindingFlags.Static | BindingFlags.NonPublic);
            }

            public static void KeyDownHelper(KeyEventArgs e, DependencyObject findToolBarHost)
                => s_keyDownHelperMethod.Invoke(null, new object[] { e, findToolBarHost });
        }

        internal class FlowControlReflector
        {
            private static readonly Dictionary<Type, FlowControlReflector> s_flowControlReflectorLookup;

            static FlowControlReflector()
            {
                var types = new List<Type> { typeof(FlowDocumentPageViewer), typeof(FlowDocumentScrollViewer), typeof(FlowDocumentReader) };
                s_flowControlReflectorLookup = types.ToDictionary(t => t, t => new FlowControlReflector(t));
            }

            private readonly PropertyInfo _canShowFindToolBarProperty;

            private readonly FieldInfo _findToolBarHostField;

            public FlowControlReflector(Type t)
            {
                _canShowFindToolBarProperty = t.GetProperty("CanShowFindToolBar", BindingFlags.Instance | BindingFlags.NonPublic);
                _findToolBarHostField = t.GetField("_findToolBarHost", BindingFlags.Instance | BindingFlags.NonPublic);
            }

            public bool CanShowFindToolBar(object flowControl) => (bool)_canShowFindToolBarProperty.GetValue(flowControl);

            public Decorator GetFindToolBarHost(object flowControl) => _findToolBarHostField.GetValue(flowControl) as Decorator;

            public void SetFindToolBarHost(object flowControl, Decorator value) => _findToolBarHostField.SetValue(flowControl, value);

            public static FlowControlReflector GetReflector(IEnhancedFlowControl enhancedFlowControl)
            {
                Type type = enhancedFlowControl.GetType();
                return s_flowControlReflectorLookup.First(kvp => type.IsSubclassOf(kvp.Key)).Value;
            }
        }

        private readonly Action<Action> _dispatcher;
        private FlowControlReflector _flowControlReflector;
        private FrameworkElement _findToolBarContent;
        private Decorator _originalFindToolBarHost;
        private IEnhancedFlowControl _flowControl;
        private FindToolBarViewModel _findToolBarViewModel;

        public FindToolBarManager(Action<Action> dispatcher = null) => _dispatcher = dispatcher;

        private bool IsShowingFindToolbar => _originalFindToolBarHost.Child != null;

        internal void Shim(IEnhancedFlowControl flowControl, FrameworkElement findToolBarContent)
        {
            Type testType = flowControl.GetType();
            _flowControlReflector = FlowControlReflector.GetReflector(flowControl);
            _findToolBarContent = findToolBarContent;
            _originalFindToolBarHost = _flowControlReflector.GetFindToolBarHost(flowControl);
            _flowControl = flowControl;
            var shimFindToolBarHost = new ShimFindToolBarHost();
            shimFindToolBarHost.ShowToolBarEvent += ShimFindToolBarHost_ShowToolBarEvent;
            shimFindToolBarHost.CloseToolBarEvent += ShimFindToolBarHost_CloseToolBarEvent;
            _flowControlReflector.SetFindToolBarHost(flowControl, shimFindToolBarHost);
        }

        // includes relevant parts of DocumentViewerHelper.ToggleFindToolBar
        private void ShimFindToolBarHost_CloseToolBarEvent(object sender, EventArgs e)
        {
            _findToolBarViewModel = null;
            _originalFindToolBarHost.Child = null;
            _originalFindToolBarHost.Visibility = Visibility.Collapsed;
            KeyboardNavigation.SetTabNavigation(_originalFindToolBarHost, KeyboardNavigationMode.None);
            _originalFindToolBarHost.ClearValue(FocusManager.IsFocusScopeProperty);
        }

        // includes relevant parts of DocumentViewerHelper.ToggleFindToolBar
        private void ShimFindToolBarHost_ShowToolBarEvent(object sender, ToolBar findToolBar)
        {
            _findToolBarViewModel = new FindToolBarViewModel(new FindToolbarWrapper(findToolBar), _flowControl as FrameworkElement);
            _findToolBarContent.DataContext = _findToolBarViewModel;

            _originalFindToolBarHost.Child = _findToolBarContent;
            _originalFindToolBarHost.Visibility = Visibility.Visible;
            KeyboardNavigation.SetTabNavigation(_originalFindToolBarHost, KeyboardNavigationMode.Continue);
            FocusManager.SetIsFocusScope(_originalFindToolBarHost, true);

            ReadyTextBox();
        }

        private void ReadyTextBox()
        {
            TextBox findTextBox = VisualTreeUtilities.FindByName<TextBox>(_findToolBarContent, "findTextBox");
            if (findTextBox == null)
            {
                return;
            }

            findTextBox.PreviewKeyDown += (sender, e) =>
            {
                if (e == null || (e.Key != Key.Return && e.Key != Key.Execute))
                {
                    return;
                }

                e.Handled = true;
                _findToolBarViewModel.Find();
            };

            GoToTextBox(findTextBox);
        }

        private void GoToTextBox(TextBox findTextBox)
        {
            Action doGoToTextBox = () =>
            {
                _ = findTextBox.Focus();
                _ = Keyboard.Focus(findTextBox);
            };

            if (_dispatcher != null)
            {
                _dispatcher(doGoToTextBox);
            }
            else
            {
                _ = findTextBox.Dispatcher.BeginInvoke(doGoToTextBox, DispatcherPriority.Background);
            }
        }

        internal void KeyDown(KeyEventArgs e)
        {
            if (e.Key != Key.F3 || !_flowControlReflector.CanShowFindToolBar(_flowControl) || !IsShowingFindToolbar)
            {
                return;
            }

            bool searchUp = (e.KeyboardDevice.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift;
            _findToolBarViewModel.Find(searchUp);
            e.Handled = true;
        }

        internal static void KeyDownHandler(object sender, KeyEventArgs e)
        {
            Decorator originalFindToolBarHost = (sender as IEnhancedFlowControl).FindToolBarManager._originalFindToolBarHost;
            DocumentViewHelper.KeyDownHelper(e, originalFindToolBarHost);
        }
    }
}
