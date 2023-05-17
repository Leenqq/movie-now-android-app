using MovieNowApp.Models;
using MovieNowApp.Services;
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
    [QueryProperty(nameof(MovieId), "movieId")]
    public class MovieDetailViewModel : BaseViewModel
    {
        private int _userId;
        private int _movieId;
        private double _averageRating;
        private int _ratingsCount;
        private Rating _userRating;

        private Movie _movie;
        private List<Rating> _ratings;

        private MovieService _movieService = new MovieService();
        private RatingService _ratingService = new RatingService();

        public ICommand RateMovieCommand { get; }

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

        public int MovieId
        {
            get
            {
                return _movieId;
            }
            set
            {
                _movieId = value;
                OnPropertyChanged(nameof(MovieId));
                LoadData();
            }
        }

        public double AverageRating
        {
            get
            {
                return _averageRating;
            }
            set
            {
                _averageRating = value;
                OnPropertyChanged(nameof(AverageRating));
            }
        }

        public int RatingsCount
        {
            get
            {
                return _ratingsCount;
            }
            set
            {
                _ratingsCount = value;
                OnPropertyChanged(nameof(RatingsCount));
            }
        }

        public Rating UserRating
        {
            get
            {
                return _userRating;
            }
            set
            {
                _userRating = value;
                OnPropertyChanged(nameof(UserRating));
            }
        }

        public Movie Movie
        {
            get
            {
                return _movie;
            }
            set
            {
                _movie = value;
                OnPropertyChanged(nameof(Movie));
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
                GetAverageRating();
                GetUserRating();
            }
        }


        public MovieDetailViewModel()
        {
            UserId = Preferences.Get("user_id", 0);
            RateMovieCommand = new Command(RateMovie);
        }

        public async void LoadData()
        {
            Movie = await _movieService.GetMovieById(MovieId);
            Ratings = await _ratingService.GetRatingsByMovieId(MovieId);
        }

        public async void GetAverageRating()
        {
            RatingsCount = Ratings.Count;
            int sum = 0;

            foreach(Rating r in Ratings)
            {
                sum += (int)r.RatingNumber;
            }

            if (RatingsCount > 0)
            {
                AverageRating = sum / RatingsCount;
            }
            else
            {
                AverageRating = 0;
            }
        }

        public async void GetUserRating()
        {
            UserRating = Ratings.Where(x => x.UserId == UserId).FirstOrDefault();
        }

        public async void RateMovie()
        {
            var rate = await App.Current.MainPage.DisplayActionSheet("Choose your rating!", "Cancel", null, "1", "2", "3", "4", "5", "Delete rating");

            if (rate != "Cancel")
            {
                if (rate == "Delete rating")
                {
                    await _ratingService.DeleteRating(UserRating.Id);
                }
                else
                {
                    if (UserRating == null)
                    {
                        await _ratingService.CreateRating(
                            new Rating
                            {
                                UserId = UserId,
                                MovieId = MovieId,
                                RatingNumber = Int32.Parse(rate)
                            }); ;
                    }
                    else
                    {
                        await _ratingService.UpdateRating(
                            new Rating
                            {
                                Id = UserRating.Id,
                                UserId = UserId,
                                MovieId = MovieId,
                                RatingNumber = Int32.Parse(rate)
                            });
                    }
                }
                LoadData();
            }
        }
    }
}
