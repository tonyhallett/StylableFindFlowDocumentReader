using System.Windows;
using System.Windows.Controls;

namespace StylableFindFlowDocumentReader.FindControls
{
    public class FindToolBar : ToolBar
    {
        static FindToolBar() => DefaultStyleKeyProperty.OverrideMetadata(
                typeof(FindToolBar),
                new FrameworkPropertyMetadata(typeof(FindToolBar)));
    }
}
