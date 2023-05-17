using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MovieNowApp.ViewModels
{
    public class SearchMovieViewModel : BaseViewModel
    {
        private bool _isActionChecked;
        private bool _isAdventureChecked;
        private bool _isAnimationChecked;
        private bool _isComedyChecked;
        private bool _isCrimeChecked;
        private bool _isDramaChecked;
        private bool _isFantasyChecked;
        private bool _isHorrorChecked;
        private bool _isMysteryChecked;
        private bool _isRomanceChecked;
        private bool _isSciFiChecked;
        private bool _isShortChecked;
        private bool _isThrillerChecked;
        private string _title;
        private string _year;

        public ICommand SearchCommand { get; }

        public bool IsActionChecked
        {
            get
            {
                return _isActionChecked;
            }
            set
            {
                _isActionChecked = value;
                OnPropertyChanged(nameof(IsActionChecked));
            }
        }
        public bool IsAdventureChecked
        {
            get
            {
                return _isAdventureChecked;
            }
            set
            {
                _isAdventureChecked = value;
                OnPropertyChanged(nameof(IsAdventureChecked));
            }
        }
        public bool IsAnimationChecked
        {
            get
            {
                return _isAnimationChecked;
            }
            set
            {
                _isAnimationChecked = value;
                OnPropertyChanged(nameof(IsAnimationChecked));
            }
        }
        public bool IsComedyChecked
        {
            get
            {
                return _isComedyChecked;
            }
            set
            {
                _isComedyChecked = value;
                OnPropertyChanged(nameof(IsComedyChecked));
            }
        }
        public bool IsCrimeChecked
        {
            get
            {
                return _isCrimeChecked;
            }
            set
            {
                _isCrimeChecked = value;
                OnPropertyChanged(nameof(IsCrimeChecked));
            }
        }
        public bool IsDramaChecked
        {
            get
            {
                return _isDramaChecked;
            }
            set
            {
                _isDramaChecked = value;
                OnPropertyChanged(nameof(IsDramaChecked));
            }
        }
        public bool IsFantasyChecked
        {
            get
            {
                return _isFantasyChecked;
            }
            set
            {
                _isFantasyChecked = value;
                OnPropertyChanged(nameof(IsFantasyChecked));
            }
        }
        public bool IsHorrorChecked
        {
            get
            {
                return _isHorrorChecked;
            }
            set
            {
                _isHorrorChecked = value;
                OnPropertyChanged(nameof(IsHorrorChecked));
            }
        }
        public bool IsMysteryChecked
        {
            get
            {
                return _isMysteryChecked;
            }
            set
            {
                _isMysteryChecked = value;
                OnPropertyChanged(nameof(IsMysteryChecked));
            }
        }
        public bool IsRomanceChecked
        {
            get
            {
                return _isRomanceChecked;
            }
            set
            {
                _isRomanceChecked = value;
                OnPropertyChanged(nameof(IsRomanceChecked));
            }
        }
        public bool IsSciFiChecked
        {
            get
            {
                return _isSciFiChecked;
            }
            set
            {
                _isSciFiChecked = value;
                OnPropertyChanged(nameof(IsSciFiChecked));
            }
        }
        public bool IsShortChecked
        {
            get
            {
                return _isShortChecked;
            }
            set
            {
                _isShortChecked = value;
                OnPropertyChanged(nameof(IsShortChecked));
            }
        }
        public bool IsThrillerChecked
        {
            get
            {
                return _isThrillerChecked;
            }
            set
            {
                _isThrillerChecked = value;
                OnPropertyChanged(nameof(IsThrillerChecked));
            }
        }
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string Year
        {
            get
            {
                return _year;
            }
            set
            {
                _year = value;
                OnPropertyChanged(nameof(Year));
            }
        }

        public SearchMovieViewModel()
        {
            SearchCommand = new Command(SearchMovies);
        }

        private async void SearchMovies()
        {
            await Shell.Current.GoToAsync($"..?title={Title}&genre={CreateGenreString()}&year={Year}&find=yes");
        }

        private string CreateGenreString()
        {
            string result = "";

            if (IsActionChecked)
            {
                result += "action,";
            }
            if (IsAdventureChecked)
            {
                result += "adventure,";
            }
            if (IsAnimationChecked)
            {
                result += "animation,";
            }
            if (IsComedyChecked)
            {
                result += "comedy,";
            }
            if (IsCrimeChecked)
            {
                result += "crime,";
            }
            if (IsDramaChecked)
            {
                result += "drama,";
            }
            if (IsFantasyChecked)
            {
                result += "fantasy,";
            }
            if (IsHorrorChecked)
            {
                result += "horror,";
            }
            if (IsMysteryChecked)
            {
                result += "mystery,";
            }
            if (IsRomanceChecked)
            {
                result += "romance,";
            }
            if (IsSciFiChecked)
            {
                result += "sci-fi,";
            }
            if (IsShortChecked)
            {
                result += "short,";
            }
            if (IsThrillerChecked)
            {
                result += "thriller,";
            }

            if (result.Length > 0)
            {
                return result.Remove(result.Length - 1, 1);
            }
            else
            {
                return null;
            }
        }
    }

}
