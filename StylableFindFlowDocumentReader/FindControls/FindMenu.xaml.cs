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

        public Brush MenuBorder
        {
            get { return (Brush)GetValue(MenuBorderProperty); }
            set { SetValue(MenuBorderProperty, value); }
        }

        public static readonly DependencyProperty MenuBorderProperty =
            DependencyProperty.Register(nameof(MenuBorder), typeof(Brush), typeof(FindMenu));

        #region Selected menu item
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
        #endregion

        #region Menu item
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

        
        public Brush MenuItemForeground
        {
            get { return (Brush)GetValue(MenuItemForegroundProperty); }
            set { SetValue(MenuItemForegroundProperty, value); }
        }

        public static readonly DependencyProperty MenuItemForegroundProperty =
            DependencyProperty.Register(nameof(MenuItemForeground), typeof(Brush), typeof(FindMenu));

        public Brush MenuItemHoverForeground
        {
            get { return (Brush)GetValue(MenuItemHoverForegroundProperty); }
            set { SetValue(MenuItemHoverForegroundProperty, value); }
        }

        public static readonly DependencyProperty MenuItemHoverForegroundProperty =
            DependencyProperty.Register(nameof(MenuItemHoverForeground), typeof(Brush), typeof(FindMenu));

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
        #endregion

        #region Drop down
        public Brush DropDownGlyphBrush
        {
            get { return (Brush)GetValue(DropDownGlyphBrushProperty); }
            set { SetValue(DropDownGlyphBrushProperty, value); }
        }

        public static readonly DependencyProperty DropDownGlyphBrushProperty =
            DependencyProperty.Register(nameof(DropDownGlyphBrush), typeof(Brush), typeof(FindMenu));

        public Brush DropDownGlyphMouseOver
        {
            get { return (Brush)GetValue(DropDownGlyphMouseOverProperty); }
            set { SetValue(DropDownGlyphMouseOverProperty, value); }
        }

        public static readonly DependencyProperty DropDownGlyphMouseOverProperty =
            DependencyProperty.Register(nameof(DropDownGlyphMouseOver), typeof(Brush), typeof(FindMenu));

        public Brush DropDownGlyphKeyboardFocused
        {
            get { return (Brush)GetValue(DropDownGlyphKeyboardFocusedProperty); }
            set { SetValue(DropDownGlyphKeyboardFocusedProperty, value); }
        }

        public static readonly DependencyProperty DropDownGlyphKeyboardFocusedProperty =
            DependencyProperty.Register(nameof(DropDownGlyphKeyboardFocused), typeof(Brush), typeof(FindMenu));

        public Brush DropDownGlyphMouseOverFocused
        {
            get { return (Brush)GetValue(DropDownGlyphMouseOverFocusedProperty); }
            set { SetValue(DropDownGlyphMouseOverFocusedProperty, value); }
        }

        public static readonly DependencyProperty DropDownGlyphMouseOverFocusedProperty =
            DependencyProperty.Register(nameof(DropDownGlyphMouseOverFocused), typeof(Brush), typeof(FindMenu));

        public Brush DropDownGlyphPressed
        {
            get { return (Brush)GetValue(DropDownGlyphPressedProperty); }
            set { SetValue(DropDownGlyphPressedProperty, value); }
        }

        public static readonly DependencyProperty DropDownGlyphPressedProperty =
            DependencyProperty.Register(nameof(DropDownGlyphPressed), typeof(Brush), typeof(FindMenu));

        public Brush DropDownStateGlyph
        {
            get { return (Brush)GetValue(DropDownStateGlyphProperty); }
            set { SetValue(DropDownStateGlyphProperty, value); }
        }

        public static readonly DependencyProperty DropDownStateGlyphProperty =
            DependencyProperty.Register(nameof(DropDownStateGlyph), typeof(Brush), typeof(FindMenu));

        public Brush DropDownGlyphDisabledBrush
        {
            get { return (Brush)GetValue(DropDownGlyphDisabledBrushProperty); }
            set { SetValue(DropDownGlyphDisabledBrushProperty, value); }
        }

        public static readonly DependencyProperty DropDownGlyphDisabledBrushProperty =
            DependencyProperty.Register(nameof(DropDownGlyphDisabledBrush), typeof(Brush), typeof(FindMenu));

        /// <summary>
        /// Considered as fallback for IsMouseOver, IsKeyboardFocused 
        /// </summary>
        public Brush DropDownStateBackground
        {
            get { return (Brush)GetValue(DropDownStateBackgroundProperty); }
            set { SetValue(DropDownStateBackgroundProperty, value); }
        }

        public static readonly DependencyProperty DropDownStateBackgroundProperty =
            DependencyProperty.Register(nameof(DropDownStateBackground), typeof(Brush), typeof(FindMenu));

        public Brush DropDownBackgroundPressed
        {
            get { return (Brush)GetValue(DropDownBackgroundPressedProperty); }
            set { SetValue(DropDownBackgroundPressedProperty, value); }
        }

        public static readonly DependencyProperty DropDownBackgroundPressedProperty =
            DependencyProperty.Register(nameof(DropDownBackgroundPressed), typeof(Brush), typeof(FindMenu));

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

        public Brush DropDownBackgroundMouseOverFocused
        {
            get { return (Brush)GetValue(DropDownBackgroundMouseOverFocusedProperty); }
            set { SetValue(DropDownBackgroundMouseOverFocusedProperty, value); }
        }

        public static readonly DependencyProperty DropDownBackgroundMouseOverFocusedProperty =
            DependencyProperty.Register(nameof(DropDownBackgroundMouseOverFocused), typeof(Brush), typeof(FindMenu));

        public Brush DropDownStateBorder
        {
            get { return (Brush)GetValue(DropDownStateBorderProperty); }
            set { SetValue(DropDownStateBorderProperty, value); }
        }

        public static readonly DependencyProperty DropDownStateBorderProperty =
            DependencyProperty.Register(nameof(DropDownStateBorder), typeof(Brush), typeof(FindMenu));

        public Brush DropDownBorderPressed
        {
            get { return (Brush)GetValue(DropDownBorderPressedProperty); }
            set { SetValue(DropDownBorderPressedProperty, value); }
        }

        public static readonly DependencyProperty DropDownBorderPressedProperty =
            DependencyProperty.Register(nameof(DropDownBorderPressed), typeof(Brush), typeof(FindMenu));

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

        public Brush DropDownBorderMouseOverFocused
        {
            get { return (Brush)GetValue(DropDownBorderMouseOverFocusedProperty); }
            set { SetValue(DropDownBorderMouseOverFocusedProperty, value); }
        }

        public static readonly DependencyProperty DropDownBorderMouseOverFocusedProperty =
            DependencyProperty.Register(nameof(DropDownBorderMouseOverFocused), typeof(Brush), typeof(FindMenu));

        #region Tooltip
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
        #endregion
        #endregion
        public FindMenu()
        {
            InitializeComponent();
        }
    }
}
