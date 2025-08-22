using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using StylableFindFlowDocumentReader.Utils;

namespace StylableFindFlowDocumentReader.Reader
{
    internal sealed class FindToolBarShimDecorator : Decorator
    {
        private readonly ToolBar _findToolBar;
        private readonly Decorator _originalDecorator;
        private readonly FindToolBarViewModel _findToolBarViewModel;

        public FindToolBarShimDecorator(Decorator originalDecorator, FrameworkElement findToolbarContent, FindRestylingFlowDocumentReader findRestylingFlowDocumentReader)
        {
            _findToolBar = originalDecorator.Child as ToolBar;
            _originalDecorator = originalDecorator;
            _findToolBarViewModel = new FindToolBarViewModel(new FindToolbarWrapper(_findToolBar), findRestylingFlowDocumentReader);
            findToolbarContent.DataContext = _findToolBarViewModel;
            _originalDecorator.Child = findToolbarContent;
            originalDecorator.Replace(this);
            base.Child = _originalDecorator;
        }

        public void Find(bool searchUp) => _findToolBarViewModel.Find(searchUp);

        public void Find() => _findToolBarViewModel.Find();

        public override UIElement Child
        {
            get => ReturnOriginalFindToolBar(new StackTrace()) ? _findToolBar : (UIElement)_originalDecorator;

            set => base.Child = value;
        }

        private static bool ReturnOriginalFindToolBar(StackTrace stackTrace)
            => stackTrace.TypeIsACaller<FlowDocumentReader>();

        internal void ResetOriginalDecorator() => this.Replace(_originalDecorator);
    }
}
