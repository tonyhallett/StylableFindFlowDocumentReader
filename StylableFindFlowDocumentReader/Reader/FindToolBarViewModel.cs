using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using StylableFindFlowDocumentReader.Commands;

namespace StylableFindFlowDocumentReader.Reader
{
    public class FindToolBarViewModel : INotifyPropertyChanged, IFindParameters
    {
        private readonly IFinder _finder;
        private string _findText;
        private bool _isSearchUp;
        private bool _matchWholeWord;
        private bool _matchCase;
        private bool _matchDiacritic;
        private bool _matchKashida;
        private bool _matchAlefHamza;
        private object _originalDataContext;

        public object OriginalDataContext
        {
            get => _originalDataContext;
            set
            {
                if (_originalDataContext == value)
                {
                    return;
                }

                _originalDataContext = value;
                OnPropertyChanged(nameof(OriginalDataContext));
            }
        }

        public FindToolBarViewModel(IFinder finder, FrameworkElement originalDataContextElement)
        {
            _finder = finder;
            NextCommand = new RelayCommand(ExecuteNext, CanExecuteFind);
            PreviousCommand = new RelayCommand(ExecutePrevious, CanExecuteFind);

            OriginalDataContext = originalDataContextElement.DataContext;
            originalDataContextElement.DataContextChanged += (s, e) => OriginalDataContext = originalDataContextElement.DataContext;
        }

        public string FindText
        {
            get => _findText;
            set
            {
                if (_findText == value)
                {
                    return;
                }

                _findText = value;
                OnPropertyChanged(nameof(FindText));
                RaiseCommandCanExecuteChanged();
            }
        }

        public bool MatchWholeWord
        {
            get => _matchWholeWord;
            set
            {
                if (_matchWholeWord == value)
                {
                    return;
                }

                _matchWholeWord = value;
            }
        }

        public bool MatchCase
        {
            get => _matchCase;
            set
            {
                if (_matchCase == value)
                {
                    return;
                }

                _matchCase = value;
            }
        }

        public bool MatchDiacritic
        {
            get => _matchDiacritic;
            set
            {
                if (_matchDiacritic == value)
                {
                    return;
                }

                _matchDiacritic = value;
            }
        }

        public bool MatchKashida
        {
            get => _matchKashida;
            set
            {
                if (_matchKashida == value)
                {
                    return;
                }

                _matchKashida = value;
            }
        }

        public bool MatchAlefHamza
        {
            get => _matchAlefHamza;
            set
            {
                if (_matchAlefHamza == value)
                {
                    return;
                }

                _matchAlefHamza = value;
            }
        }

        public bool IsSearchUp
        {
            get => _isSearchUp;
            private set
            {
                if (_isSearchUp == value)
                {
                    return;
                }

                _isSearchUp = value;
                OnPropertyChanged(nameof(IsSearchUp));
                OnPropertyChanged(nameof(IsSearchDown));
            }
        }

        public bool IsSearchDown => !IsSearchUp;

        public ICommand NextCommand { get; }

        public ICommand PreviousCommand { get; }

        private void ExecuteNext() => Find(false);

        private void ExecutePrevious() => Find(true);

        internal void Find(bool searchUp)
        {
            IsSearchUp = searchUp;
            Find();
        }

        internal void Find() => _finder.Find(this);

        private bool CanExecuteFind() => !string.IsNullOrWhiteSpace(FindText);

        private void RaiseCommandCanExecuteChanged()
        {
            (NextCommand as RelayCommand).NotifyCanExecuteChanged();
            (PreviousCommand as RelayCommand).NotifyCanExecuteChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
