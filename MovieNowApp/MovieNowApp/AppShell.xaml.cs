using MovieNowApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieNowApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = this;
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(SearchMoviePage), typeof(SearchMoviePage));
            Routing.RegisterRoute(nameof(MovieDetailPage), typeof(MovieDetailPage));
            Routing.RegisterRoute(nameof(UserRatingsPage), typeof(UserRatingsPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        }

        public ICommand LogoutCommand => new Command(async () =>
        {
            Preferences.Remove("user_id");
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        });
    }
}