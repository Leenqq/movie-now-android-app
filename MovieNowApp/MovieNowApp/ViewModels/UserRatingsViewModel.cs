using MovieNowApp.Models;
using MovieNowApp.Services;
using MovieNowApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MovieNowApp.ViewModels
{
    public class UserRatingsViewModel : BaseViewModel
    {
        private int _userId;
        private bool _isSortedDesc = false;

        private RatedMovie _selectedRatedMovie;

        private List<RatedMovie> _ratedMovies;

        private MovieService _movieService = new MovieService();

        public ICommand SortByRatingCommand { get; }

        public ICommand SortByTitleCommand { get; }

        public ICommand ToMovieDetailCommand { get; }

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

        public bool IsSortedDesc
        {
            get
            {
                return _isSortedDesc;
            }
            set
            {
                _isSortedDesc = value;
                OnPropertyChanged(nameof(IsSortedDesc));
            }
        }

        public RatedMovie SelectedRatedMovie
        {
            get
            {
                return _selectedRatedMovie;
            }
            set
            {
                _selectedRatedMovie = value;
                OnPropertyChanged(nameof(SelectedRatedMovie));
                if (_selectedRatedMovie != null)
                {
                    GoToMovieDetails(SelectedRatedMovie.Id);
                    SelectedRatedMovie = null;
                }
            }
        }

        public List<RatedMovie> RatedMovies
        {
            get
            {
                return _ratedMovies;
            }
            set
            {
                _ratedMovies = value;
                OnPropertyChanged(nameof(RatedMovies));
            }
        }

        public UserRatingsViewModel()
        {
            UserId = Preferences.Get("user_id", 0);
            SortByRatingCommand = new Command(SortByRating);
            SortByTitleCommand = new Command(SortByTitle);
            ToMovieDetailCommand = new Command(GoToMovieDetails);
            LoadData();
        }

        private void SortByRating(object obj)
        {
            if (IsSortedDesc)
            {
                RatedMovies = RatedMovies.OrderBy(x => x.Rating).ToList();
                IsSortedDesc = false;
            }
            else
            {
                RatedMovies = RatedMovies.OrderByDescending(x => x.Rating).ToList();
                IsSortedDesc = true;
            }
        }

        private void SortByTitle(object obj)
        {
            if (IsSortedDesc)
            {
                RatedMovies = RatedMovies.OrderBy(x => x.Title).ToList();
                IsSortedDesc = false;
            }
            else
            {
                RatedMovies = RatedMovies.OrderByDescending(x => x.Title).ToList();
                IsSortedDesc = true;
            }
        }

        private async void GoToMovieDetails(object id)
        {
            await Shell.Current.GoToAsync($"/{nameof(MovieDetailPage)}?movieId={(int)id}");
        }

        public async void LoadData()
        {
            RatedMovies = await _movieService.GetRatedMoviesByUser(UserId);
        }
    }
}
