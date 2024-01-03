using CommunityToolkit.Mvvm.Input;
using StoreManagementSystemX.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels
{
    public class LoginViewModel: BaseViewModel
    {
        public LoginViewModel(IAuthenticationService authenticationService, IDialogService dialogService) {
            _username = string.Empty;
            _authenticationService = authenticationService;
            _dialogService = dialogService;
        }

        private readonly IAuthenticationService _authenticationService;
        private readonly IDialogService _dialogService;

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }


        public bool Login(string password)
        {
            try
            {
                _authenticationService.Login(Username, password);
                return true;
            }
            catch
            {
                _dialogService.ShowMessageDialog("Login Failed", "Username or password is incorrect");
            }

            return false;
        }

        

    }
}
