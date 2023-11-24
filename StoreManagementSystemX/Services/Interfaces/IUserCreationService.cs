using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Services.Interfaces
{
    public interface IUserCreationService
    {
        public Guid? CreateNewUser(AuthContext authContext);
    }
}
