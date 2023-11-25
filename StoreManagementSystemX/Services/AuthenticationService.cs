using StoreManagementSystemX.Database.DAL;
using StoreManagementSystemX.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Services
{
    class AuthenticationService : IAuthenticationService
    {
        private readonly UnitOfWork _unitOfWork;


        public AuthenticationService(IDialogService dialogService)
        {
            _unitOfWork = new UnitOfWork();
            _dialogService = dialogService;
            AuthContext = null;
        }

        public AuthContext? AuthContext { get; private set; }
        private IDialogService _dialogService;
        public bool IsAuthenticated { get => AuthContext != null; }

        public void Login(string username, string password)
        {
            var storedUser = _unitOfWork.UserRepository.Find(u => u.Username == username && u.Password == password);
            if(storedUser != null)
            {
                AuthContext = new AuthContext(storedUser);
            } else
            {
                throw new Exception("Username or password is incorrect");
            }

        }

        public void Logout()
        {
            if (AuthContext == null)
                throw new InvalidOperationException("No user is authenticated");

            AuthContext = null;

        }
    }
}
