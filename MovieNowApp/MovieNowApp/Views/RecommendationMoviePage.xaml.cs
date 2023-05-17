using MovieNowApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieNowApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecommendationMoviePage : ContentPage
    {
        RecommendationMovieViewModel _viewModel;
        public RecommendationMoviePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new RecommendationMovieViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadData();
        }
    }
}