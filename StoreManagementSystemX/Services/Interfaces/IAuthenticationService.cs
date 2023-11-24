using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Services.Interfaces
{


    public interface IAuthenticationService
    {
        public AuthContext? AuthContext { get; }

        public bool IsAuthenticated { get; }

        public void Login(string username, string password);

        public void Logout();

    }
}
