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
    public partial class LoginPage : ContentPage
    {
        LoginViewModel _viewModel;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new LoginViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.Username = null;
            _viewModel.Password = null;
            _viewModel.ErrorMessage = null;
        }
    }
}