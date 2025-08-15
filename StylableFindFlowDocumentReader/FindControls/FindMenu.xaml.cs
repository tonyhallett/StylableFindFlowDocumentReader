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

        public Brush MenuBorderBrush
        {
            get { return (Brush)GetValue(MenuBorderBrushProperty); }
            set { SetValue(MenuBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty MenuBorderBrushProperty =
            DependencyProperty.Register(nameof(MenuBorderBrush), typeof(Brush), typeof(FindMenu));

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

        public Brush SelectedGlyphBorderBrush
        {
            get { return (Brush)GetValue(SelectedGlyphBorderBrushProperty); }
            set { SetValue(SelectedGlyphBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty SelectedGlyphBorderBrushProperty =
            DependencyProperty.Register(nameof(SelectedGlyphBorderBrush), typeof(Brush), typeof(FindMenu));
        #endregion

        #region Menu item
        public Brush MenuItemBackground
        {
            get { return (Brush)GetValue(MenuItemBackgroundProperty); }
            set { SetValue(MenuItemBackgroundProperty, value); }
        }

        public static readonly DependencyProperty MenuItemBackgroundProperty =
            DependencyProperty.Register(nameof(MenuItemBackground), typeof(Brush), typeof(FindMenu));

        public Brush MenuItemHighlightedBackground
        {
            get { return (Brush)GetValue(MenuItemHighlightedBackgroundProperty); }
            set { SetValue(MenuItemHighlightedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty MenuItemHighlightedBackgroundProperty =
            DependencyProperty.Register(nameof(MenuItemHighlightedBackground), typeof(Brush), typeof(FindMenu));

        
        public Brush MenuItemForeground
        {
            get { return (Brush)GetValue(MenuItemForegroundProperty); }
            set { SetValue(MenuItemForegroundProperty, value); }
        }

        public static readonly DependencyProperty MenuItemForegroundProperty =
            DependencyProperty.Register(nameof(MenuItemForeground), typeof(Brush), typeof(FindMenu));

        public Brush MenuItemHighlightedForeground
        {
            get { return (Brush)GetValue(MenuItemHighlightedForegroundProperty); }
            set { SetValue(MenuItemHighlightedForegroundProperty, value); }
        }

        public static readonly DependencyProperty MenuItemHighlightedForegroundProperty =
            DependencyProperty.Register(nameof(MenuItemHighlightedForeground), typeof(Brush), typeof(FindMenu));

        public Brush MenuItemBorderBrush
        {
            get { return (Brush)GetValue(MenuItemBorderBrushProperty); }
            set { SetValue(MenuItemBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty MenuItemBorderBrushProperty =
            DependencyProperty.Register(nameof(MenuItemBorderBrush), typeof(Brush), typeof(FindMenu));

        public Brush MenuItemHighlightedBorderBrush
        {
            get { return (Brush)GetValue(MenuItemHighlightedBorderBrushProperty); }
            set { SetValue(MenuItemHighlightedBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty MenuItemHighlightedBorderBrushProperty =
            DependencyProperty.Register(nameof(MenuItemHighlightedBorderBrush), typeof(Brush), typeof(FindMenu));
        #endregion

        #region Drop down
        public Brush DropDownGlyphBrush
        {
            get { return (Brush)GetValue(DropDownGlyphBrushProperty); }
            set { SetValue(DropDownGlyphBrushProperty, value); }
        }

        public static readonly DependencyProperty DropDownGlyphBrushProperty =
            DependencyProperty.Register(nameof(DropDownGlyphBrush), typeof(Brush), typeof(FindMenu));

        public Brush DropDownGlyphMouseOverBrush
        {
            get { return (Brush)GetValue(DropDownGlyphMouseOverBrushProperty); }
            set { SetValue(DropDownGlyphMouseOverBrushProperty, value); }
        }

        public static readonly DependencyProperty DropDownGlyphMouseOverBrushProperty =
            DependencyProperty.Register(nameof(DropDownGlyphMouseOverBrush), typeof(Brush), typeof(FindMenu));

        public Brush DropDownGlyphFocusedBrush
        {
            get { return (Brush)GetValue(DropDownGlyphFocusedBrushProperty); }
            set { SetValue(DropDownGlyphFocusedBrushProperty, value); }
        }

        public static readonly DependencyProperty DropDownGlyphFocusedBrushProperty =
            DependencyProperty.Register(nameof(DropDownGlyphFocusedBrush), typeof(Brush), typeof(FindMenu));

        public Brush DropDownGlyphMouseOverFocusedBrush
        {
            get { return (Brush)GetValue(DropDownGlyphMouseOverFocusedBrushProperty); }
            set { SetValue(DropDownGlyphMouseOverFocusedBrushProperty, value); }
        }

        public static readonly DependencyProperty DropDownGlyphMouseOverFocusedBrushProperty =
            DependencyProperty.Register(nameof(DropDownGlyphMouseOverFocusedBrush), typeof(Brush), typeof(FindMenu));

        public Brush DropDownGlyphPressedBrush
        {
            get { return (Brush)GetValue(DropDownGlyphPressedBrushProperty); }
            set { SetValue(DropDownGlyphPressedBrushProperty, value); }
        }

        public static readonly DependencyProperty DropDownGlyphPressedBrushProperty =
            DependencyProperty.Register(nameof(DropDownGlyphPressedBrush), typeof(Brush), typeof(FindMenu));

        public Brush DropDownStateGlyphBrush
        {
            get { return (Brush)GetValue(DropDownStateGlyphBrushProperty); }
            set { SetValue(DropDownStateGlyphBrushProperty, value); }
        }

        public static readonly DependencyProperty DropDownStateGlyphBrushProperty =
            DependencyProperty.Register(nameof(DropDownStateGlyphBrush), typeof(Brush), typeof(FindMenu));

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

        public Brush DropDownBackgroundFocused
        {
            get { return (Brush)GetValue(DropDownBackgroundFocusedProperty); }
            set { SetValue(DropDownBackgroundFocusedProperty, value); }
        }

        public static readonly DependencyProperty DropDownBackgroundFocusedProperty =
            DependencyProperty.Register(nameof(DropDownBackgroundFocused), typeof(Brush), typeof(FindMenu));

        public Brush DropDownBackgroundMouseOverFocused
        {
            get { return (Brush)GetValue(DropDownBackgroundMouseOverFocusedProperty); }
            set { SetValue(DropDownBackgroundMouseOverFocusedProperty, value); }
        }

        public static readonly DependencyProperty DropDownBackgroundMouseOverFocusedProperty =
            DependencyProperty.Register(nameof(DropDownBackgroundMouseOverFocused), typeof(Brush), typeof(FindMenu));

        public Brush DropDownStateBorderBrush
        {
            get { return (Brush)GetValue(DropDownStateBorderBrushProperty); }
            set { SetValue(DropDownStateBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty DropDownStateBorderBrushProperty =
            DependencyProperty.Register(nameof(DropDownStateBorderBrush), typeof(Brush), typeof(FindMenu));

        public Brush DropDownBorderPressedBrush
        {
            get { return (Brush)GetValue(DropDownBorderPressedBrushProperty); }
            set { SetValue(DropDownBorderPressedBrushProperty, value); }
        }

        public static readonly DependencyProperty DropDownBorderPressedBrushProperty =
            DependencyProperty.Register(nameof(DropDownBorderPressedBrush), typeof(Brush), typeof(FindMenu));

        public Brush DropDownBorderMouseOverBrush
        {
            get { return (Brush)GetValue(DropDownBorderMouseOverBrushProperty); }
            set { SetValue(DropDownBorderMouseOverBrushProperty, value); }
        }

        public static readonly DependencyProperty DropDownBorderMouseOverBrushProperty =
            DependencyProperty.Register(nameof(DropDownBorderMouseOverBrush), typeof(Brush), typeof(FindMenu));

        public Brush DropDownBorderFocusedBrush
        {
            get { return (Brush)GetValue(DropDownBorderFocusedBrushProperty); }
            set { SetValue(DropDownBorderFocusedBrushProperty, value); }
        }

        public static readonly DependencyProperty DropDownBorderFocusedBrushProperty =
            DependencyProperty.Register(nameof(DropDownBorderFocusedBrush), typeof(Brush), typeof(FindMenu));

        public Brush DropDownBorderMouseOverFocusedBrush
        {
            get { return (Brush)GetValue(DropDownBorderMouseOverFocusedBrushProperty); }
            set { SetValue(DropDownBorderMouseOverFocusedBrushProperty, value); }
        }

        public static readonly DependencyProperty DropDownBorderMouseOverFocusedBrushProperty =
            DependencyProperty.Register(nameof(DropDownBorderMouseOverFocusedBrush), typeof(Brush), typeof(FindMenu));

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
