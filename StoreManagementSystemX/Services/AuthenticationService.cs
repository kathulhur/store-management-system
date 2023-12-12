using StoreManagementSystemX.Database.DAL;
using StoreManagementSystemX.Domain.Aggregates.Roots.Users.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Users.Interfaces;
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
        private readonly IUserRepository _userRepository;


        public AuthenticationService(IUserRepository userRepository, IDialogService dialogService)
        {
            _userRepository = userRepository;
            _dialogService = dialogService;
            AuthContext = null;
        }

        public AuthContext? AuthContext { get; private set; }
        private IDialogService _dialogService;
        public bool IsAuthenticated { get => AuthContext != null; }

        public void Login(string username, string password)
        {
            var storedUser = _userRepository.GetByUsernameAndPassword(username, password);

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
