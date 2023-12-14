using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Users.Interfaces;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.Views.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Services
{
    public class UserCreationService : IUserCreationService
    {
        public UserCreationService(Domain.Repositories.Users.Interfaces.IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private readonly Domain.Repositories.Users.Interfaces.IUserRepository _userRepository;

        public Guid? CreateNewUser(AuthContext authContext)
        {
            Guid? newUserId = null;
            var window = new CreateUserWindow(authContext, _userRepository, (Guid id) =>
            {
                newUserId = id;
            });

            window.ShowDialog();

            return newUserId;
        }


    }
}
