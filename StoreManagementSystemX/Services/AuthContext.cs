using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using StoreManagementSystemX.Domain.Aggregates.Roots.Users.Interfaces;
using StoreManagementSystemX.Services;
using StoreManagementSystemX.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Services
{
    public class AuthContext
    {
        public AuthContext(IUser authenticatedUser)
        {
            CurrentUser = new AuthUser(authenticatedUser);
        }

        public AuthUser CurrentUser { get; }

        public class AuthUser
        {
            public AuthUser(IUser user) 
            {
                _user = user;
            }

            private readonly IUser _user;

            public Guid Id => _user.Id;
        }
    }
}
