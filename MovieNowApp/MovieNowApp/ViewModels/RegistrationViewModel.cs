using MovieNowApp.Models;
using MovieNowApp.Services;
using MovieNowApp.Views;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MovieNowApp.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private string _errorMessage;

        UserService _userService = new UserService();

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

        public RegistrationViewModel()
        {
            RegisterCommand = new Command(Register);
        }

        private async void Register(object obj)
        {
            User user;
            if (Username == null || Password == null)
            {
                ErrorMessage = "* Username and password required!";
            }
            else
            {
                if (Password.Length >= 5)
                {
                    user = new User()
                    {
                        Username = Username,
                        Password = Password
                    };

                    var result = await _userService.CreateUser(user);

                    if (result == HttpStatusCode.Created)
                    {
                        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                    }
                    else
                    {
                        ErrorMessage = "* Something wrong...";
                    }
                }
                else
                {
                    ErrorMessage = "* Password must have more than 5 characters!";
                }
            }
        }
    }
}
