using System.Windows.Media;

namespace Demo
{
    internal class Palette { 
        public Brush MainBackground { get; set; }
        public Brush MainForeground { get; set; }
        public Brush Background { get; set; }
        public Brush Text { get; set; }
        public Brush Border { get; set; }
        public Brush PrevNext { get; set; } = Brushes.Transparent;

        public Brush DropDownGlyphBrush { get; set; }
        public Brush DropDownStateBackground { get; set; }
        public Brush DropDownStateBorder { get; set; }
        public Brush DropDownStateGlyph { get; set; }

        public Brush MenuItemBackground { get; set; }
        public Brush MenuItemBorderBrush { get; set; }
        public Brush MenuItemForeground { get; set; }
        public Brush MenuItemHoverBackground { get; set; }
        public Brush MenuItemHoverBorderBrush { get; set; }
        public Brush MenuItemHoverForeground { get; set; }

        public Brush SelectedGlyphBackground { get; set; }
        public Brush SelectedGlyphBorder { get; set; }
        public Brush SelectedGlyphBrush { get; set; }

        public Brush TooltipBackground { get; set; }
        public Brush TooltipForeground { get; set; }
        public Brush TooltipBorder { get; set; }
    }
}
