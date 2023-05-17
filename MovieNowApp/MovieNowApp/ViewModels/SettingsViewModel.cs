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
    public class SettingsViewModel : BaseViewModel
    {
        private int _userId;
        private string _currentPassword;
        private string _confirmedPassword;
        private string _newPassword;
        private string _errorMessage;
        private string _changePasswordButtonText = "Change password";
        private bool _isPasswordNotChanging = true;

        private User _user;

        UserService _userService = new UserService(); 

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

        public string CurrentPassword
        {
            get
            {
                return _currentPassword;
            }
            set
            {
                _currentPassword = value;
                OnPropertyChanged(nameof(CurrentPassword));
            }
        }

        public string ConfirmedPassword
        {
            get
            {
                return _confirmedPassword;
            }
            set
            {
                _confirmedPassword = value;
                OnPropertyChanged(nameof(ConfirmedPassword));
            }
        }

        public string NewPassword
        {
            get
            {
                return _newPassword;
            }
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
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

        public string ChangePasswordButtonText
        {
            get
            {
                return _changePasswordButtonText;
            }
            set
            {
                _changePasswordButtonText = value;
                OnPropertyChanged(nameof(ChangePasswordButtonText));
            }
        }

        public bool IsPasswordNotChanging
        {
            get
            {
                return _isPasswordNotChanging;
            }
            set
            {
                _isPasswordNotChanging = value;
                OnPropertyChanged(nameof(IsPasswordNotChanging));
            }
        }

        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public ICommand ChangePasswordCommand { get; }

        public ICommand DeleteAccountCommand { get; }

        public SettingsViewModel()
        {
            UserId = Preferences.Get("user_id", 0);
            ChangePasswordCommand = new Command(ChangePassword);
            DeleteAccountCommand = new Command(DeleteAccount);
        }

        private async void DeleteAccount()
        {
            bool comfirm = await App.Current.MainPage.DisplayAlert("Delete account", "Are you sure?", "Yes!", "No!");

            if (comfirm)
            {
                await _userService.DeleteUser(UserId);
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
        }

        private async void ChangePassword()
        {
            if (IsPasswordNotChanging)
            {
                ChangePasswordButtonText = "Confirm";
                IsPasswordNotChanging = false;
            }
            else
            {
                if (CurrentPassword != null || ConfirmedPassword != null || NewPassword != null)
                {
                    if (CurrentPassword == User.Password)
                    {
                        if (CurrentPassword == ConfirmedPassword)
                        {
                            if (NewPassword.Length >= 5)
                            {
                                User = await _userService.GetUserById(UserId);
                                var newUser = new User
                                {
                                    Id = User.Id,
                                    Username = User.Username,
                                    Password = NewPassword
                                };
                                await _userService.UpdateUser(newUser);
                                ChangePasswordButtonText = "Change password";
                                IsPasswordNotChanging = true;
                                await App.Current.MainPage.DisplayAlert("Change password", "Password changed", "Ok!");
                            }
                            else
                            {
                                ErrorMessage = "* Password must have more than 5 characters!";
                            }
                        }
                        else
                        {
                            ErrorMessage = "* Passwords do not match!";
                        }
                    }
                    else
                    {
                        ErrorMessage = "* Wrong password!";
                    }
                }
                else
                {
                    ErrorMessage = "* All fields are required!";
                }
            }
        }
    }
}
