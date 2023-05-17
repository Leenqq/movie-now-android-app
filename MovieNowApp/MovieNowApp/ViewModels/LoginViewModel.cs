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
    public class LoginViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private string _errorMessage;

        UserService _userService = new UserService();

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public LoginViewModel()
        {
            LoginCommand = new Command(Login);
            RegisterCommand = new Command(Register);
        }

        private async void Login(object obj)
        {
            User user = await _userService.GetUserByUsername(Username);

            if (user != null && user.Password == Password)
            {
                Preferences.Set("user_id", user.Id);
                await Shell.Current.GoToAsync($"//{nameof(MoviesListPage)}?userId={user.Id}");
            }
            else
            {
                ErrorMessage = "* Invalid credentials";
            }
        }

        private async void Register(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync($"/{nameof(RegistrationPage)}");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
