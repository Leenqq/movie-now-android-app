using MovieNowApp.Models;
using MovieNowApp.Services;
using MovieNowApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MovieNowApp.ViewModels
{
    public class RecommendationMovieViewModel : BaseViewModel
    {
        private int _userId;
        private bool _isRecommendationExist;

        RecommendationService _recommendationService = new RecommendationService();
        RatingService _ratingService = new RatingService();

        private List<Movie> _recommendMovies;
        private List<Rating> _ratings;

        public ICommand RecommendCommand { get; }

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

        public bool IsRecommendationExist
        {
            get
            {
                return _isRecommendationExist;
            }
            set
            {
                _isRecommendationExist = value;
                OnPropertyChanged(nameof(IsRecommendationExist));
            }
        }

        public List<Movie> RecommendMovies
        {
            get
            {
                return _recommendMovies;
            }
            set
            {
                _recommendMovies = value;
                OnPropertyChanged(nameof(RecommendMovies));

                if (RecommendMovies != null)
                {
                    IsRecommendationExist = true;
                }
                else
                {
                    IsRecommendationExist = false;
                }
            }
        }

        public List<Rating> Ratings
        {
            get
            {
                return _ratings;
            }
            set
            {
                _ratings = value;
                OnPropertyChanged(nameof(Ratings));
            }
        }

        public RecommendationMovieViewModel()
        {
            UserId = Preferences.Get("user_id", 0);
            RecommendCommand = new Command(GetRecommendation);
            ToMovieDetailCommand = new Command(GoToMovieDetails);
            LoadData();
        }

        private async void GetRecommendation(object obj)
        {
            if (Ratings.Count >= 5)
            {
                RecommendMovies = await _recommendationService.GetRecommendationsByUserId(UserId);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Recommendation", "You must rate at least 5 movies!\n ", "Ok");
            }
        }

        private async void GoToMovieDetails(object id)
        {
            await Shell.Current.GoToAsync($"/{nameof(MovieDetailPage)}?movieId={(int)id}");
        }

        public async void LoadData()
        {
            Ratings = await _ratingService.GetRatingsByUserId(UserId);
        }
    }

}
