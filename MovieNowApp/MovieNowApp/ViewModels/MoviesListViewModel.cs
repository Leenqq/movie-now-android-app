using MovieNowApp.Models;
using MovieNowApp.Services;
using MovieNowApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MovieNowApp.ViewModels
{
    [QueryProperty(nameof(Title), "title")]
    [QueryProperty(nameof(Genre), "genre")]
    [QueryProperty(nameof(Year), "year")]
    [QueryProperty(nameof(Find), "find")]
    public class MoviesListViewModel : BaseViewModel
    {
        private int _userId;
        private string _title;
        private string _genre;
        private string _year;
        private string _find;
        private bool _isSearchResultExist;

        private Movie _selectedSearchResult;

        private ObservableCollection<Movie> _recentMovies;
        private ObservableCollection<Movie> _searchResult = new ObservableCollection<Movie>();

        MovieService _movieService = new MovieService();

        public ICommand CarouselItemTapped { get; }
        public ICommand OnSearchButton { get; }

        public int UserId
        {
            get 
            { 
                return _userId; 
            }
            set
            {
                _userId = value;
                OnPropertyChanged(nameof(UserId));
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

        public string Genre
        {
            get
            {
                return _genre;
            }
            set
            {
                _genre = value;
                OnPropertyChanged(nameof(Genre));
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

        public string Find
        {
            get
            {
                return _find;
            }
            set
            {
                _find = value;
                OnPropertyChanged(nameof(Find));
                if (Find != null)
                {
                    FindMovies();
                }
            }
        }

        public bool IsSearchResultExist
        {
            get
            {
                return _isSearchResultExist;
            }
            set
            {
                _isSearchResultExist = value;
                OnPropertyChanged(nameof(IsSearchResultExist));
            }
        }

        public Movie SelectedSearchResult
        {
            get
            {
                return _selectedSearchResult;
            }
            set
            {
                _selectedSearchResult = value;
                OnPropertyChanged(nameof(SelectedSearchResult));
                if (_selectedSearchResult != null)
                {
                    GoToMovieDetails(SelectedSearchResult.Id);
                    SelectedSearchResult = null;
                }
            }
        }

        public ObservableCollection<Movie> RecentMovies
        {
            get
            {
                return _recentMovies;
            }
            set
            {
                _recentMovies = value;
                OnPropertyChanged(nameof(RecentMovies));
            }
        }

        public ObservableCollection<Movie> SearchResult
        {
            get
            {
                return _searchResult;
            }
            set
            {
                _searchResult = value;
                OnPropertyChanged(nameof(SearchResult));
                
                if(SearchResult != null)
                {
                    IsSearchResultExist = true;
                }
                else
                {
                    IsSearchResultExist = false;
                }
            }
        }

        public MoviesListViewModel()
        {
            UserId = Preferences.Get("user_id", 0);
            CarouselItemTapped = new Command(GoToMovieDetails);
            OnSearchButton = new Command(GoToSearchMovie);
            GetRecentMovies();
        }
        private async Task GetRecentMovies()
        {
            RecentMovies = new ObservableCollection<Movie>(await _movieService.GetRecentMovies());
        }

        private async void GoToMovieDetails(object id)
        {
            await Shell.Current.GoToAsync($"/{nameof(MovieDetailPage)}?movieId={(int)id}");
            
        }

        private async void GoToSearchMovie()
        {
            await Shell.Current.GoToAsync($"/{nameof(SearchMoviePage)}");
        }

        private async void FindMovies()
        {
            SearchResult.Clear();

            var movies = new ObservableCollection<Movie>(await _movieService.FilterMovies(Title, Genre, Year));

            if (movies.Count > 0)
            {
                SearchResult = movies;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Searh", "No results!", "Ok");
            }
        }
    }
}
