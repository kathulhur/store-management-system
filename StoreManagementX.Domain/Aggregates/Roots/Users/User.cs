using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using StoreManagementSystemX.Domain.Aggregates.Roots.Users.Interfaces;
using StoreManagementSystemX.Domain.Factories.Products;
using StoreManagementSystemX.Domain.Factories.Products.Interfaces;
using StoreManagementSystemX.Domain.Factories.StockPurchases;
using StoreManagementSystemX.Domain.Factories.StockPurchases.Interfaces;
using StoreManagementSystemX.Domain.Factories.Transactions;
using StoreManagementSystemX.Domain.Factories.Transactions.Interfaces;
using StoreManagementSystemX.Domain.Factories.Users;
using StoreManagementSystemX.Domain.Factories.Users.Interfaces;
using StoreManagementSystemX.Domain.Services.Interfaces;

namespace StoreManagementSystemX.Domain.Aggregates.Roots.Users
{
    public class User : IUser
    {

        internal User(Guid creatorId, Guid id, string username, string password, IUserFactory userFactory, IProductFactory productFactory, ITransactionFactory transactionFactory, IStockPurchaseFactory stockPurchaseFactory)
        {
            CreatorId = creatorId;
            Id = id;
            Username = username;
            Password = password;
            UserFactory = userFactory;
            ProductFactory = productFactory;
            TransactionFactory = transactionFactory;
            StockPurchaseFactory = stockPurchaseFactory;
        }

        public Guid CreatorId { get; }

        public Guid Id { get; }

        public string Username { get; set; }

        public IUserFactory UserFactory { get; internal set; }

        public IProductFactory ProductFactory { get; internal set; }

        public ITransactionFactory TransactionFactory { get; internal set; }

        public IStockPurchaseFactory StockPurchaseFactory { get; internal set; }

        internal string Password;

        public void ChangePassword(string password)
        {
            Password = password;
        }


        public override string ToString()
            => $"Created By: {CreatorId} - ID: {Id} - Username: {Username} - Password: {Password}";


        public override bool Equals(object? obj)
        {
            return obj is User user &&
                   Id.Equals(user.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
