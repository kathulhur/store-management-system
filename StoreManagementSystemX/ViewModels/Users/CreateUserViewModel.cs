using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StoreManagementSystemX.Database.DAL;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
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
    public class CreateUserViewModel : ObservableObject, ICreateUserViewModel
    {
        public CreateUserViewModel(AuthContext authContext, IUnitOfWorkFactory unitOfWorkFactory, Action<Guid> onCreate, Action close)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _authContext = authContext;
            _onCreate = onCreate;
            _close = close;
            _submitCommand = new RelayCommand<string>(SubmitCommandHandler, CanSubmitCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandHandler);
        }

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        private readonly AuthContext _authContext;

        private Action<Guid> _onCreate;
        private Action _close;

        private string _username;
        public string Username { get => _username; set => SetProperty(ref _username, value); }

        private RelayCommand<string> _submitCommand;
        public ICommand SubmitCommand => _submitCommand;

        public ICommand CancelCommand { get; }

        private bool CanSubmitCommandExecute(string? password)
            => Username.Length > 0 && password != null && password.Length > 0;

        private void SubmitCommandHandler(string? password)
        {
            if (password != null && password.Length > 0)
            {
                Console.WriteLine("password: " + password);
                User newUser = new User { Id = Guid.NewGuid(), CreatedById = _authContext.CurrentUser.Id, Username = Username, Password = password };
                using(var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
                {
                    unitOfWork.UserRepository.Insert(newUser);
                    unitOfWork.Save();
                    _onCreate(newUser.Id);
                    _close();

                }
            }
        }

        private void CancelCommandHandler()
        {
            _close();
        }




    }
}
