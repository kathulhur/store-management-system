using StoreManagementSystemX.Domain.Aggregates.Roots.Users;
using StoreManagementSystemX.Domain.Aggregates.Roots.Users.Interfaces;
using StoreManagementSystemX.Domain.Factories.Products;
using StoreManagementSystemX.Domain.Factories.StockPurchases;
using StoreManagementSystemX.Domain.Factories.Transactions;
using StoreManagementSystemX.Domain.Factories.Users.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Factories.Users
{
    public class UserFactory : IUserFactory
    {
        public IUser Create(ICreateUserArgs createUserArgs)
        {
            return new User(createUserArgs.CreatorId, Guid.NewGuid(), createUserArgs.Username, createUserArgs.Password, new UserFactory(), new ProductFactory(), new TransactionFactory(), new StockPurchaseFactory());
        }
    }
}
