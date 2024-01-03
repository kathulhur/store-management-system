using StoreManagementSystemX.Domain.Factories.Products.Interfaces;
using StoreManagementSystemX.Domain.Factories.StockPurchases.Interfaces;
using StoreManagementSystemX.Domain.Factories.Transactions.Interfaces;
using StoreManagementSystemX.Domain.Factories.Users.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Aggregates.Roots.Users.Interfaces
{
    public interface IUser
    {
        public Guid Id { get; }

        public Guid CreatorId { get; }

        public string Username { get; set; }

        public string Password { get; }

        public void ChangePassword(string password);

        public IUserFactory UserFactory { get; }

        public IProductFactory ProductFactory { get; }

        public ITransactionFactory TransactionFactory { get; }

        public IStockPurchaseFactory StockPurchaseFactory { get; }

    }
}
