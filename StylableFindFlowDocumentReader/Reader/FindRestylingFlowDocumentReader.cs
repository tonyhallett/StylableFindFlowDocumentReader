using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace StylableFindFlowDocumentReader
{
    public class FindRestylingFlowDocumentReader : FlowDocumentReader
    {
        private Decorator _originalFindToolBarHost;
        private Decorator _contentHost;
        private FindToolBarShimDecorator _shimFindToolbarHost;
        private KeyCommandHandler _keyHandler;

        public static readonly DependencyProperty FindToolbarContentProperty =
            DependencyProperty.Register(
                nameof(FindToolbarContent),
                typeof(FrameworkElement),
                typeof(FindRestylingFlowDocumentReader),
                new PropertyMetadata(null));

        public ScrollBarVisibility VerticalScrollbarVisibility
        {
            get { return (ScrollBarVisibility)GetValue(VerticalScrollbarVisibilityProperty); }
            set { SetValue(VerticalScrollbarVisibilityProperty, value); }
        }

        public static readonly DependencyProperty VerticalScrollbarVisibilityProperty =
            DependencyProperty.Register(nameof(VerticalScrollbarVisibility), typeof(ScrollBarVisibility), typeof(FindRestylingFlowDocumentReader), new PropertyMetadata(ScrollBarVisibility.Visible, VerticalScrollbarVisibilityChanged));

        private static void VerticalScrollbarVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as FindRestylingFlowDocumentReader).TrySetVerticalScrollbarVisibility((ScrollBarVisibility)e.NewValue);
        }

        private void TrySetVerticalScrollbarVisibility(ScrollBarVisibility scrollBarVisibility)
        {
            if(_contentHost != null && ViewingMode == FlowDocumentReaderViewingMode.Scroll)
            {
                SetVerticalScrollbarVisibility(scrollBarVisibility);
            }
        }

        private void SetVerticalScrollbarVisibility(ScrollBarVisibility scrollBarVisibility)
        {
            var flowDocumentScrollViewer = _contentHost.Child as FlowDocumentScrollViewer;
            if (flowDocumentScrollViewer != null)
            {
                flowDocumentScrollViewer.VerticalScrollBarVisibility = scrollBarVisibility;
            }
        }

        public FrameworkElement FindToolbarContent
        {
            get => (FrameworkElement)GetValue(FindToolbarContentProperty);
            set => SetValue(FindToolbarContentProperty, value);
        }

        public FindRestylingFlowDocumentReader()
        {
            Loaded += (sender, e) => _keyHandler = new KeyCommandHandler(this);
            Unloaded += (sender, e) =>
            {
                _keyHandler?.Detach();
                _keyHandler = null;
            };
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _originalFindToolBarHost = GetTemplateChild("PART_FindToolBarHost") as Decorator;
             _contentHost = GetTemplateChild("PART_ContentHost") as Decorator;
        }

        public bool IsShowingFindToolbar { get; private set; }

        public void ExecuteFindCommand() => ApplicationCommands.Find.Execute(null, this);

        protected override void OnFindCommand()
        {
            DoWork();
        }

        private void DoWork(bool invokeBaseOnFindCommand = true)
        {
            bool removing = _shimFindToolbarHost != null;
            IsShowingFindToolbar = !removing;
            if(invokeBaseOnFindCommand)
            {
                base.OnFindCommand();
            }
            
            if (Document == null || !IsFindEnabled)
            {
                return;
            }

            if (removing)
            {
                Restore();
                return;
            }

            if (!(_originalFindToolBarHost.Child is ToolBar findToolBar))
            {
                return;
            }

            if (FindToolbarContent != null)
            {
                SetShimFindToolBarHost();
            }
            else
            {
                DoRestyleFindToolBar(findToolBar);
            }
        }

        private void Restore()
        {
            _originalFindToolBarHost.Child = null;
            MoveHostProperties(true);
            _shimFindToolbarHost.ResetOriginalDecorator();
            ResetOriginalToolbarHost();
            _shimFindToolbarHost = null;
            IsShowingFindToolbar = false;
        }

        private static FieldInfo s_findToolBarHostField;

        private static FieldInfo FindToolBarHostField
        {
            get
            {
                if (s_findToolBarHostField == null)
                {
                    s_findToolBarHostField = typeof(FlowDocumentReader).GetField("_findToolBarHost", BindingFlags.Instance | BindingFlags.NonPublic);
                }

                return s_findToolBarHostField;
            }
        }

        private void SetShimFindToolBarHost()
        {
            _shimFindToolbarHost = new FindToolBarShimDecorator(_originalFindToolBarHost, FindToolbarContent);
            FindToolBarHostField.SetValue(this, _shimFindToolbarHost);
            CopyHostProperties(_originalFindToolBarHost, _shimFindToolbarHost);
            MoveHostProperties(false);
            ReadyTextBox();
        }

        private void ReadyTextBox()
        {
            TextBox findTextBox = VisualTreeUtilities.FindByName<TextBox>(FindToolbarContent, "findTextBox");
            if (findTextBox == null)
            {
                return;
            }

            findTextBox.PreviewKeyDown += (object sender, KeyEventArgs e) =>
            {
                if (e == null || (e.Key != Key.Return && e.Key != Key.Execute))
                {
                    return;
                }

                e.Handled = true;
                _shimFindToolbarHost.Find();
            };

            GoToTextBox(findTextBox);
        }

        private void GoToTextBox(TextBox findTextBox)
            => DoDispatch(() =>
            {
                _ = findTextBox.Focus();
                _ = Keyboard.Focus(findTextBox);
            });

        protected virtual void DoDispatch(Action action) 
            => _ = Dispatcher.BeginInvoke(action, DispatcherPriority.Background);

        protected void CopyHostProperties(Decorator originalHost,Decorator replacementHost)
        {
            replacementHost.HorizontalAlignment = originalHost.HorizontalAlignment;
            replacementHost.VerticalAlignment = originalHost.VerticalAlignment;
        }

        private void MoveHostProperties(bool resetting)
        {
            Decorator fromDecorator = resetting ? _shimFindToolbarHost : _originalFindToolBarHost;
            Decorator toDecorator = resetting ? _originalFindToolBarHost : _shimFindToolbarHost;
            toDecorator.Visibility = fromDecorator.Visibility;
            KeyboardNavigation.SetTabNavigation(toDecorator, KeyboardNavigation.GetTabNavigation(fromDecorator));
            FocusManager.SetIsFocusScope(toDecorator, FocusManager.GetIsFocusScope(fromDecorator));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Handled)
            {
                return;
            }
            
            switch (e.Key)
            {
                case Key.Escape:
                    // go to base
                    break;
                case Key.F3:
                    if (_shimFindToolbarHost != null && IsFindEnabled)
                    {
                        bool searchUp = (e.KeyboardDevice.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift;
                        _shimFindToolbarHost.Find(searchUp);
                        e.Handled = true;
                    }

                    break;
            }

            if (e.Handled)
            {
                return;
            }

            base.OnKeyDown(e);
            if(e.Key == Key.F3 && _shimFindToolbarHost == null)
            {
                DoWork(false);
                return;
            }
            if (e.Key != Key.Escape || _shimFindToolbarHost == null || !IsFindEnabled)
            {
                return;
            }

            Restore();
        }

        private void ResetOriginalToolbarHost() => FindToolBarHostField.SetValue(this, _originalFindToolBarHost);

        protected virtual void DoRestyleFindToolBar(ToolBar findToolBar)
        {
            RestyleFindToolBar(findToolBar);
            if (!(findToolBar.FindName("FindTextBoxBorder") is Border findTextBorder))
            {
                return;
            }

            RestyleFindTextBoxBorder(findTextBorder);

            Grid findTextBoxGrid = findTextBorder.FindName("FindTextBoxGrid") as Grid;
            Label findTextLabel = findTextBorder.FindName("FindTextLabel") as Label;
            TextBox findTextBox = findTextBorder.FindName("FindTextBox") as TextBox;
            RestyleFindTextControls(findTextBox, findTextLabel, findTextBoxGrid);

            var findPreviousButton = findTextBorder.FindName("FindPreviousButton") as Button;
            var findNextButton = findTextBorder.FindName("FindNextButton") as Button;
            if (findPreviousButton != null || findNextButton != null)
            {
                RestyleFindButtons(findNextButton, findPreviousButton);
            }

            if (!(findTextBorder.FindName("OptionsMenu") is Menu menu))
            {
                return;
            }

            if (!(menu.FindName("OptionsMenuItem") is MenuItem menuItem))
            {
                return;
            }

            RestyleOptionsMenu(menu, menuItem);
        }

        /// <summary>
        /// Applies style findToolBarToolBarStyle if finds it FindResource.
        /// </summary>
        /// <param name="findToolBar">The FindToolBar to restyle.</param>
        protected virtual void RestyleFindToolBar(ToolBar findToolBar) => ApplyRestyle(findToolBar);

        /// <summary>
        /// Invokes the individual methods if not null.
        /// </summary>
        /// <param name="findTextBox">The TextBox to restyle.</param>
        /// <param name="findLabel">The Label to restyle.</param>
        /// <param name="findGrid">The Grid to restyle</param>
        protected virtual void RestyleFindTextControls(TextBox findTextBox, Label findLabel, Grid findGrid)
        {
            if (findTextBox != null)
            {
                RestyleFindTextBox(findTextBox);
            }

            if (findLabel != null)
            {
                RestyleFindTextLabel(findLabel);
            }

            if (findGrid == null)
            {
                return;
            }

            RestyleFindGrid(findGrid);
        }

        protected virtual void RestyleFindGrid(Grid grid) => ApplyRestyle(grid);


        // this has BorderBrush "{DynamicResource {x:Static JetSystemColors.ControlDarkBrushKey}}"
        // and Background that is LinearGradient with GotFocus background gradient stop animations

        /// <summary>
        /// Applies style findToolBarBorderStyle if finds it FindResource.
        /// </summary>
        /// <param name="findTextBorder">The Border to restyle.</param>
        protected virtual void RestyleFindTextBoxBorder(Border findTextBorder) => ApplyRestyle(findTextBorder);

        // Default Foreground is GrayTextBrushKey.

        /// <summary>
        /// Applies style findToolBarLabelStyle if finds it FindResource.
        /// </summary>
        /// <param name="findTextLabel">The placeholder Label to restyle.</param>
        protected virtual void RestyleFindTextLabel(Label findTextLabel) => ApplyRestyle(findTextLabel);

        // Default Background is Transparent.

        /// <summary>
        /// Applies style findToolBarTextBoxStyle if finds it FindResource.
        /// </summary>
        /// <param name="findTextBox">The TextBox to restyle.</param>
        protected virtual void RestyleFindTextBox(TextBox findTextBox) => ApplyRestyle(findTextBox);

        // Default style with control template that has triggers that change the Border and Background.

        /// <summary>
        /// Applies style findToolBarButtonStyle if finds it FindResource.
        /// </summary>
        /// <param name="findNextButton">The next button to restyle.</param>
        /// <param name="findPreviousButton">The previous button to restyle.</param>
        protected virtual void RestyleFindButtons(Button findNextButton, Button findPreviousButton)
        {
            RestyleButton(findNextButton);
            RestyleButton(findPreviousButton);
        }

        private void RestyleButton(Button button) => ApplyRestyle(button);

        /// <summary>
        /// Applies style findToolBarMenuStyle and findToolBarMenuItemStyle if finds it FindResource.
        /// </summary>
        /// <param name="menu">The Menu to restyle.</param>
        /// <param name="rootMenuItem">The root MenuItem to restyle.</param>
        protected virtual void RestyleOptionsMenu(Menu menu, MenuItem rootMenuItem)
        {
            ApplyRestyle(menu);
            ApplyRestyle(rootMenuItem);
        }

        protected void ApplyRestyle(FrameworkElement element, string styleSuffix = null)
        {
            if (element == null)
            {
                return;
            }

            styleSuffix = styleSuffix ?? element.GetType().Name;
            if (!(element.TryFindResource($"findToolBar{styleSuffix}Style") is Style style))
            {
                return;
            }

            element.Style = style;
        }

        protected override void SwitchViewingModeCore(FlowDocumentReaderViewingMode viewingMode)
        {
            base.SwitchViewingModeCore(viewingMode);
            if(viewingMode == FlowDocumentReaderViewingMode.Scroll)
            {
                TrySetVerticalScrollbarVisibility(VerticalScrollbarVisibility);
            }
        }
    }
}
