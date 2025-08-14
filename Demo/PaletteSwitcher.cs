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
        private Palette leftPalette = new Palette {
            MainBackground = Brushes.DarkBlue,
            MainForeground = Brushes.LightBlue,
            Background = Brushes.LightBlue,
            Text = Brushes.DarkBlue,
            Border = Brushes.DarkBlue,
            PrevNext = Brushes.DarkBlue,
            DropDownGlyphBrush = Brushes.BlueViolet,
            DropDownStateBackground = Brushes.DarkSlateBlue,
            DropDownStateBorder = Brushes.AliceBlue,
            DropDownStateGlyph = Brushes.AliceBlue,
            MenuItemBackground = Brushes.DodgerBlue,
            MenuItemBorderBrush = Brushes.DarkBlue,
            MenuItemForeground = Brushes.DarkBlue,
            MenuItemHoverBackground = Brushes.DarkBlue,
            MenuItemHoverForeground = Brushes.DodgerBlue,
            MenuItemHoverBorderBrush = Brushes.DodgerBlue,
            SelectedGlyphBrush = Brushes.RoyalBlue,
            SelectedGlyphBackground = Brushes.LightSkyBlue,
            SelectedGlyphBorder = Brushes.DarkBlue
        };
        private Palette rightPalette = new Palette { 
            MainBackground = Brushes.DeepPink,
            MainForeground = Brushes.LightPink,
            Background = Brushes.Pink,
            Text = Brushes.DeepPink,
            Border = Brushes.DeepPink,
            PrevNext = Brushes.DeepPink,
            DropDownGlyphBrush = Brushes.HotPink,
            DropDownStateBackground = Brushes.LightPink,
            DropDownStateBorder = Brushes.DeepPink,
            DropDownStateGlyph = Brushes.DeepPink,
            MenuItemBackground = Brushes.DeepPink,
            MenuItemBorderBrush = Brushes.LightPink,
            MenuItemForeground = Brushes.LightPink,
            MenuItemHoverBackground = Brushes.LightPink,
            MenuItemHoverForeground = Brushes.DeepPink,
            MenuItemHoverBorderBrush= Brushes.DeepPink,
            SelectedGlyphBrush = Brushes.MistyRose,
            SelectedGlyphBackground = Brushes.HotPink,
            SelectedGlyphBorder = Brushes.LightPink
        };
        private PaletteSwitcher()
        {
            _selectedPalette = leftPalette;
        }

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
