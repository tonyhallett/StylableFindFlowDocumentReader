using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Demo
{
    internal class PaletteSwitcher : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
    => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public static PaletteSwitcher Instance { get; } = new PaletteSwitcher();

        private Palette _selectedPalette;
        private readonly Palette leftPalette = new Palette
        {
            MainBackground = Brushes.DarkBlue,
            MainForeground = Brushes.LightBlue,
            Background = Brushes.LightBlue,
            Text = Brushes.DarkBlue,
            Border = Brushes.DarkBlue,
            PrevNext = Brushes.DarkBlue,
            DropDownGlyphBrush = Brushes.BlueViolet,
            DropDownStateBackground = Brushes.DarkSlateBlue,
            DropDownStateBorderBrush = Brushes.AliceBlue,
            DropDownStateGlyphBrush = Brushes.AliceBlue,
            MenuBackground = Brushes.LightBlue,
            MenuBorderBrush = Brushes.SteelBlue,
            MenuItemBackground = Brushes.DodgerBlue,
            MenuItemBorderBrush = Brushes.DarkBlue,
            MenuItemForeground = Brushes.DarkBlue,
            MenuItemHighlightedBackground = Brushes.DarkBlue,
            MenuItemHighlightedForeground = Brushes.DodgerBlue,
            MenuItemHighlightedBorderBrush = Brushes.DodgerBlue,
            SelectedGlyphBrush = Brushes.RoyalBlue,
            SelectedGlyphBackground = Brushes.LightSkyBlue,
            SelectedGlyphBorderBrush = Brushes.DarkBlue,
            TooltipBackground = Brushes.LightBlue,
            TooltipForeground = Brushes.DarkBlue,
            TooltipBorder = Brushes.DarkBlue,
        };
        private readonly Palette rightPalette = new Palette
        {
            MainBackground = Brushes.DeepPink,
            MainForeground = Brushes.LightPink,
            Background = Brushes.Pink,
            Text = Brushes.DeepPink,
            Border = Brushes.DeepPink,
            PrevNext = Brushes.DeepPink,
            DropDownGlyphBrush = Brushes.HotPink,
            DropDownStateBackground = Brushes.LightPink,
            DropDownStateBorderBrush = Brushes.DeepPink,
            DropDownStateGlyphBrush = Brushes.DeepPink,
            MenuBackground = Brushes.LightSalmon,
            MenuBorderBrush = Brushes.DeepPink,
            MenuItemBackground = Brushes.DeepPink,
            MenuItemBorderBrush = Brushes.LightPink,
            MenuItemForeground = Brushes.LightPink,
            MenuItemHighlightedBackground = Brushes.LightPink,
            MenuItemHighlightedForeground = Brushes.DeepPink,
            MenuItemHighlightedBorderBrush = Brushes.DeepPink,
            SelectedGlyphBrush = Brushes.MistyRose,
            SelectedGlyphBackground = Brushes.HotPink,
            SelectedGlyphBorderBrush = Brushes.LightPink,
            TooltipBackground = Brushes.LightPink,
            TooltipForeground = Brushes.DeepPink,
            TooltipBorder = Brushes.DeepPink,
        };
        private PaletteSwitcher() => _selectedPalette = leftPalette;

        public Palette SelectedPalette
        {
            get => _selectedPalette;
            set
            {
                if (_selectedPalette != value)
                {
                    _selectedPalette = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool LeftPaletteSelected
        {
            get => _selectedPalette == leftPalette;
            set
            {
                SelectedPalette = value ? leftPalette : rightPalette;
                OnPropertyChanged();
            }
        }

        public bool RightPaletteSelected
        {
            get => _selectedPalette == rightPalette;
            set
            {
                SelectedPalette = value ? rightPalette : leftPalette;
                OnPropertyChanged();
            }
        }
    }
}
