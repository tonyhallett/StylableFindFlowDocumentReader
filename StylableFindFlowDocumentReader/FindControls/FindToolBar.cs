using System.Windows.Controls;
using System.Windows;

namespace StylableFindFlowDocumentReader
{
    public class FindToolBar : ToolBar
    {
        static FindToolBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(FindToolBar),
                new FrameworkPropertyMetadata(typeof(FindToolBar)));
        }
    }
}
