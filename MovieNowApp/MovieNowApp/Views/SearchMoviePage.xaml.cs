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
    public partial class SearchMoviePage : ContentPage
    {
        public SearchMoviePage()
        {
            InitializeComponent();
            BindingContext = new SearchMovieViewModel();
        }
    }
}