using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StylableFindFlowDocumentReader.KeyCommands;

namespace StylableFindFlowDocumentReader.Reader
{
    public class EnhancedFlowDocumentReader : FlowDocumentReader, IEnhancedFlowControl
    {
        private Decorator _contentHost;
        private KeyCommandHandler _keyHandler;

        public FindToolBarManager FindToolBarManager { get; }

        public static readonly DependencyProperty FindToolbarContentProperty =
            DependencyProperty.Register(
                nameof(FindToolbarContent),
                typeof(FrameworkElement),
                typeof(EnhancedFlowDocumentReader),
                new PropertyMetadata(null));

        public FrameworkElement FindToolbarContent
        {
            get => (FrameworkElement)GetValue(FindToolbarContentProperty);
            set => SetValue(FindToolbarContentProperty, value);
        }

        #region VerticalScrollbarVisibility

        public ScrollBarVisibility VerticalScrollbarVisibility
        {
            get => (ScrollBarVisibility)GetValue(VerticalScrollbarVisibilityProperty);
            set => SetValue(VerticalScrollbarVisibilityProperty, value);
        }

        public static readonly DependencyProperty VerticalScrollbarVisibilityProperty =
            DependencyProperty.Register(nameof(VerticalScrollbarVisibility), typeof(ScrollBarVisibility), typeof(EnhancedFlowDocumentReader), new PropertyMetadata(ScrollBarVisibility.Visible, VerticalScrollbarVisibilityChanged));

        private static void VerticalScrollbarVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => (d as EnhancedFlowDocumentReader).TrySetVerticalScrollbarVisibility((ScrollBarVisibility)e.NewValue);

        private void TrySetVerticalScrollbarVisibility(ScrollBarVisibility scrollBarVisibility)
        {
            if (_contentHost == null || ViewingMode != FlowDocumentReaderViewingMode.Scroll)
            {
                return;
            }

            SetVerticalScrollbarVisibility(scrollBarVisibility);
        }

        private void SetVerticalScrollbarVisibility(ScrollBarVisibility scrollBarVisibility)
        {
            if (!(_contentHost.Child is FlowDocumentScrollViewer flowDocumentScrollViewer))
            {
                return;
            }

            flowDocumentScrollViewer.VerticalScrollBarVisibility = scrollBarVisibility;
        }

        protected override void SwitchViewingModeCore(FlowDocumentReaderViewingMode viewingMode)
        {
            base.SwitchViewingModeCore(viewingMode);
            if (viewingMode != FlowDocumentReaderViewingMode.Scroll)
            {
                return;
            }

            TrySetVerticalScrollbarVisibility(VerticalScrollbarVisibility);
        }

        private void SetContentHost() => _contentHost = GetTemplateChild("PART_ContentHost") as Decorator;

        #endregion

        static EnhancedFlowDocumentReader() => EventManager.RegisterClassHandler(
                typeof(EnhancedFlowDocumentReader),
                Keyboard.KeyDownEvent,
                new KeyEventHandler(StylableFindFlowDocumentReader.Reader.FindToolBarManager.KeyDownHandler),
                true);

        public EnhancedFlowDocumentReader()
        {
            Loaded += (sender, e) => _keyHandler = new KeyCommandHandler(this);
            Unloaded += (sender, e) =>
            {
                _keyHandler?.Detach();
                _keyHandler = null;
            };
            FindToolBarManager = new FindToolBarManager();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            FindToolBarManager.Shim(this, FindToolbarContent);
            SetContentHost();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            FindToolBarManager.KeyDown(e);
            base.OnKeyDown(e);
        }
    }
}
