using StoreManagementSystemX.Domain.Aggregates.Roots.Users;
using StoreManagementSystemX.Domain.Aggregates.Roots.Users.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Factories.Users.Interfaces
{
    public interface IUserFactory
    {
        public IUser Create(ICreateUserArgs createUserArgs);
    }
}
