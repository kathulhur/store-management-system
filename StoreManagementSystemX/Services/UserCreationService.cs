using StoreManagementSystemX.Database.DAL.Interfaces;
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
        public UserCreationService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public Guid? CreateNewUser(AuthContext authContext)
        {
            Guid? newUserId = null;
            using(var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var window = new CreateUserWindow(authContext, unitOfWork, (Guid id) =>
                {
                    newUserId = id;
                });

                window.ShowDialog();
            }

            return newUserId;
        }


    }
}
