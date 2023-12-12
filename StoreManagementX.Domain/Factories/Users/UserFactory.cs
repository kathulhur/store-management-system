using StoreManagementSystemX.Domain.Aggregates.Roots.Users;
using StoreManagementSystemX.Domain.Aggregates.Roots.Users.Interfaces;
using StoreManagementSystemX.Domain.Factories.Products;
using StoreManagementSystemX.Domain.Factories.Products.Interfaces;
using StoreManagementSystemX.Domain.Factories.StockPurchases;
using StoreManagementSystemX.Domain.Factories.StockPurchases.Interfaces;
using StoreManagementSystemX.Domain.Factories.Transactions;
using StoreManagementSystemX.Domain.Factories.Transactions.Interfaces;
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
        private readonly IProductFactory _productFactory;
        private readonly ITransactionFactory _transactionFactory;
        private readonly IStockPurchaseFactory _stockPurchaseFactory;


        public UserFactory(IProductFactory productFactory, ITransactionFactory transactionFactory, IStockPurchaseFactory stockPurchaseFactory)
        {
            _productFactory = productFactory;
            _transactionFactory = transactionFactory;
            _stockPurchaseFactory = stockPurchaseFactory;
        }
        public IUser Create(ICreateUserArgs createUserArgs)
        {
            return new User(createUserArgs.CreatorId, Guid.NewGuid(), createUserArgs.Username, createUserArgs.Password, this, _productFactory, _transactionFactory, _stockPurchaseFactory);
        }
    }
}
