using System.Windows;
using System.Windows.Controls;

namespace StylableFindFlowDocumentReader
{
    /// <summary>
    /// Interaction logic for FindTextBox.xaml
    /// </summary>
    public partial class FindTextBox : UserControl
    {
        public bool ShowTooltip
        {
            get { return (bool)GetValue(ShowTooltipProperty); }
            set { SetValue(ShowTooltipProperty, value); }
        }

        public static readonly DependencyProperty ShowTooltipProperty =
            DependencyProperty.Register(nameof(ShowTooltip), typeof(bool), typeof(FindTextBox), new PropertyMetadata(true));

        public string Tooltip
        {
            get { return (string)GetValue(TooltipProperty); }
            set { SetValue(TooltipProperty, value); }
        }

        public static readonly DependencyProperty TooltipProperty =
            DependencyProperty.Register(nameof(Tooltip), typeof(string), typeof(FindTextBox), new PropertyMetadata("Search for a word or phrase in this document."));

        public string HintText
        {
            get { return (string)GetValue(HintTextProperty); }
            set { SetValue(HintTextProperty, value); }
        }

        public static readonly DependencyProperty HintTextProperty =
            DependencyProperty.Register(nameof(HintText), typeof(string), typeof(FindTextBox), new PropertyMetadata("Search…"));

        public double HintOpacity
        {
            get { return (double)GetValue(HintOpacityProperty); }
            set { SetValue(HintOpacityProperty, value); }
        }

        public static readonly DependencyProperty HintOpacityProperty =
            DependencyProperty.Register(nameof(HintOpacity), typeof(double), typeof(FindTextBox), new PropertyMetadata(0.7));

        public FontStyle HintFontStyle
        {
            get { return (FontStyle)GetValue(HintFontStyleProperty); }
            set { SetValue(HintFontStyleProperty, value); }
        }

        public static readonly DependencyProperty HintFontStyleProperty =
            DependencyProperty.Register(nameof(HintFontStyle), typeof(FontStyle), typeof(FindTextBox), new PropertyMetadata(FontStyles.Italic));

        public double TextBoxWidth
        {
            get { return (double)GetValue(TextBoxWidthProperty); }
            set { SetValue(TextBoxWidthProperty, value); }
        }

        public static readonly DependencyProperty TextBoxWidthProperty =
            DependencyProperty.Register(nameof(TextBoxWidth), typeof(double), typeof(FindTextBox), new PropertyMetadata((double)183));

        public FindTextBox()
        {
            InitializeComponent();
        }
    }
}
