using StoreManagementSystemX.Domain.Aggregates.Roots.Users.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Repositories.Users.Interfaces
{
    public interface IUserRepository : IRepository<IUser>
    {
        public IUser? GetByUsernameAndPassword(string username, string password);
    }
}
