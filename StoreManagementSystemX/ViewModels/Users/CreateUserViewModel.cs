using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StoreManagementSystemX.Database.DAL;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using StoreManagementSystemX.Domain.Factories.Users.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Users.Interfaces;
using StoreManagementSystemX.Services;
using StoreManagementSystemX.ViewModels.Users.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.Users
{
    public class CreateUserViewModel : ObservableObject, ICreateUserViewModel, ICreateUserArgs
    {
        public CreateUserViewModel(
            AuthContext authContext, 
            Domain.Repositories.Users.Interfaces.IUserRepository userRepository, 
            Action<Guid> onCreate, 
            Action close)
        {
            _userRepository = userRepository;
            _authContext = authContext;
            _onCreate = onCreate;
            _close = close;
            _submitCommand = new RelayCommand<string>(SubmitCommandHandler, CanSubmitCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandHandler);
        }

        public Guid CreatorId => _authContext.CurrentUser.Id;
        private string _username;
        public string Username { get => _username; set => SetProperty(ref _username, value); }
        public string Password { get; private set; }
        
        private readonly Domain.Repositories.Users.Interfaces.IUserRepository _userRepository;

        private readonly AuthContext _authContext;

        private Action<Guid> _onCreate;
        private Action _close;


        private RelayCommand<string> _submitCommand;
        public ICommand SubmitCommand => _submitCommand;

        public ICommand CancelCommand { get; }


        private bool CanSubmitCommandExecute(string? password)
            => Username.Length > 0 && password != null && password.Length > 0;

        private void SubmitCommandHandler(string? password)
        {
            if (password != null && password.Length > 0)
            {
                Password = password;
                var newlyCreatedUser = _authContext.CurrentUser.UserFactory.Create(this);
                _userRepository.Add(newlyCreatedUser);
                _onCreate(newlyCreatedUser.Id);
                _close();
            }
        }

        private void CancelCommandHandler()
        {
            _close();
        }




    }
}
