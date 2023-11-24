using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Database.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        public IProductRepository ProductRepository { get; }

        public IUserRepository UserRepository { get; }

        public ITransactionRepository TransactionRepository { get; }

        public ITransactionProductRepository TransactionProductRepository { get; }

        public IStockPurchaseRepository StockPurchaseRepository { get; }

        public IPayLaterRepository PayLaterRepository { get; }

        public IStockPurchaseProductRepository StockPurchaseProductRepository { get; }

        public void Save();

        public void Attach(object entity);
    }
}
