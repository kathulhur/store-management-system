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
            CurrentUser = authenticatedUser;
        }

        public IUser CurrentUser { get; }


    }
}
