using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StylableFindFlowDocumentReader
{
    /// <summary>
    /// Interaction logic for FindMenu.xaml
    /// </summary>
    public partial class FindMenu : UserControl
    {
        public Brush MenuBackground
        {
            get { return (Brush)GetValue(MenuBackgroundProperty); }
            set { SetValue(MenuBackgroundProperty, value); }
        }

        public static readonly DependencyProperty MenuBackgroundProperty =
            DependencyProperty.Register(nameof(MenuBackground), typeof(Brush), typeof(FindMenu));

        public Brush MenuItemBackground
        {
            get { return (Brush)GetValue(MenuItemBackgroundProperty); }
            set { SetValue(MenuItemBackgroundProperty, value); }
        }

        public static readonly DependencyProperty MenuItemBackgroundProperty =
            DependencyProperty.Register(nameof(MenuItemBackground), typeof(Brush), typeof(FindMenu));

        public Brush MenuItemHoverBackground
        {
            get { return (Brush)GetValue(MenuItemHoverBackgroundProperty); }
            set { SetValue(MenuItemHoverBackgroundProperty, value); }
        }

        public static readonly DependencyProperty MenuItemHoverBackgroundProperty =
            DependencyProperty.Register(nameof(MenuItemHoverBackground), typeof(Brush), typeof(FindMenu));

        public Brush SelectedGlyphBrush
        {
            get { return (Brush)GetValue(SelectedGlyphBrushProperty); }
            set { SetValue(SelectedGlyphBrushProperty, value); }
        }

        public static readonly DependencyProperty SelectedGlyphBrushProperty =
            DependencyProperty.Register(nameof(SelectedGlyphBrush), typeof(Brush), typeof(FindMenu));

        public Brush SelectedGlyphBackground
        {
            get { return (Brush)GetValue(SelectedGlyphBackgroundProperty); }
            set { SetValue(SelectedGlyphBackgroundProperty, value); }
        }

        public static readonly DependencyProperty SelectedGlyphBackgroundProperty =
            DependencyProperty.Register(nameof(SelectedGlyphBackground), typeof(Brush), typeof(FindMenu));

        public Brush SelectedGlyphBorder
        {
            get { return (Brush)GetValue(SelectedGlyphBorderProperty); }
            set { SetValue(SelectedGlyphBorderProperty, value); }
        }

        public static readonly DependencyProperty SelectedGlyphBorderProperty =
            DependencyProperty.Register(nameof(SelectedGlyphBorder), typeof(Brush), typeof(FindMenu));

        public Brush MenuItemForeground
        {
            get { return (Brush)GetValue(MenuItemForegroundProperty); }
            set { SetValue(MenuItemForegroundProperty, value); }
        }

        public Brush MenuItemHoverForeground
        {
            get { return (Brush)GetValue(MenuItemHoverForegroundProperty); }
            set { SetValue(MenuItemHoverForegroundProperty, value); }
        }

        public static readonly DependencyProperty MenuItemHoverForegroundProperty =
            DependencyProperty.Register(nameof(MenuItemHoverForeground), typeof(Brush), typeof(FindMenu));

        public static readonly DependencyProperty MenuItemForegroundProperty =
            DependencyProperty.Register(nameof(MenuItemForeground), typeof(Brush), typeof(FindMenu));

        public Brush MenuItemBorderBrush
        {
            get { return (Brush)GetValue(MenuItemBorderBrushProperty); }
            set { SetValue(MenuItemBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty MenuItemBorderBrushProperty =
            DependencyProperty.Register(nameof(MenuItemBorderBrush), typeof(Brush), typeof(FindMenu));

        public Brush MenuItemHoverBorderBrush
        {
            get { return (Brush)GetValue(MenuItemHoverBorderBrushProperty); }
            set { SetValue(MenuItemHoverBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty MenuItemHoverBorderBrushProperty =
            DependencyProperty.Register(nameof(MenuItemHoverBorderBrush), typeof(Brush), typeof(FindMenu));

        public Brush MenuBorder
        {
            get { return (Brush)GetValue(MenuBorderProperty); }
            set { SetValue(MenuBorderProperty, value); }
        }

        public static readonly DependencyProperty MenuBorderProperty =
            DependencyProperty.Register(nameof(MenuBorder), typeof(Brush), typeof(FindMenu));

        public Brush DropDownGlyphBrush
        {
            get { return (Brush)GetValue(DropDownGlyphBrushProperty); }
            set { SetValue(DropDownGlyphBrushProperty, value); }
        }

        public static readonly DependencyProperty DropDownGlyphBrushProperty =
            DependencyProperty.Register(nameof(DropDownGlyphBrush), typeof(Brush), typeof(FindMenu));

        public Brush DropDownGlyphDisabledBrush
        {
            get { return (Brush)GetValue(DropDownGlyphDisabledBrushProperty); }
            set { SetValue(DropDownGlyphDisabledBrushProperty, value); }
        }

        public static readonly DependencyProperty DropDownGlyphDisabledBrushProperty =
            DependencyProperty.Register(nameof(DropDownGlyphDisabledBrush), typeof(Brush), typeof(FindMenu));

        public Brush DropDownStateBackground
        {
            get { return (Brush)GetValue(DropDownStateBackgroundProperty); }
            set { SetValue(DropDownStateBackgroundProperty, value); }
        }

        public static readonly DependencyProperty DropDownStateBackgroundProperty =
            DependencyProperty.Register(nameof(DropDownStateBackground), typeof(Brush), typeof(FindMenu));

        public Brush DropDownBackgroundMouseOver
        {
            get { return (Brush)GetValue(DropDownBackgroundMouseOverProperty); }
            set { SetValue(DropDownBackgroundMouseOverProperty, value); }
        }

        public static readonly DependencyProperty DropDownBackgroundMouseOverProperty =
            DependencyProperty.Register(nameof(DropDownBackgroundMouseOver), typeof(Brush), typeof(FindMenu));

        public Brush DropDownBackgroundKeyboardFocused
        {
            get { return (Brush)GetValue(DropDownBackgroundKeyboardFocusedProperty); }
            set { SetValue(DropDownBackgroundKeyboardFocusedProperty, value); }
        }

        public static readonly DependencyProperty DropDownBackgroundKeyboardFocusedProperty =
            DependencyProperty.Register(nameof(DropDownBackgroundKeyboardFocused), typeof(Brush), typeof(FindMenu));

        public Brush DropDownStateBorder
        {
            get { return (Brush)GetValue(DropDownStateBorderProperty); }
            set { SetValue(DropDownStateBorderProperty, value); }
        }

        public static readonly DependencyProperty DropDownStateBorderProperty =
            DependencyProperty.Register(nameof(DropDownStateBorder), typeof(Brush), typeof(FindMenu));

        public Brush DropDownBorderMouseOver
        {
            get { return (Brush)GetValue(DropDownBorderMouseOverProperty); }
            set { SetValue(DropDownBorderMouseOverProperty, value); }
        }

        public static readonly DependencyProperty DropDownBorderMouseOverProperty =
            DependencyProperty.Register(nameof(DropDownBorderMouseOver), typeof(Brush), typeof(FindMenu));

        public Brush DropDownBorderKeyboardFocused
        {
            get { return (Brush)GetValue(DropDownBorderKeyboardFocusedProperty); }
            set { SetValue(DropDownBorderKeyboardFocusedProperty, value); }
        }

        public static readonly DependencyProperty DropDownBorderKeyboardFocusedProperty =
            DependencyProperty.Register(nameof(DropDownBorderKeyboardFocused), typeof(Brush), typeof(FindMenu));

        public string DropDownTooltip
        {
            get { return (string)GetValue(DropDownTooltipProperty); }
            set { SetValue(DropDownTooltipProperty, value); }
        }

        public static readonly DependencyProperty DropDownTooltipProperty =
            DependencyProperty.Register(nameof(DropDownTooltip), typeof(string), typeof(FindMenu), new PropertyMetadata("Find..."));

        public bool ShowDropDownTooltip
        {
            get { return (bool)GetValue(ShowDropDownTooltipProperty); }
            set { SetValue(ShowDropDownTooltipProperty, value); }
        }

        public static readonly DependencyProperty ShowDropDownTooltipProperty =
            DependencyProperty.Register(nameof(ShowDropDownTooltip), typeof(bool), typeof(FindMenu), new PropertyMetadata(true));

        public FindMenu()
        {
            InitializeComponent();
        }
    }
}
