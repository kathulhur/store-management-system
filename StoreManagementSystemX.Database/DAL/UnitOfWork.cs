using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagementSystemX.Database;
using StoreManagementSystemX.Database.DAL.Interfaces;

namespace StoreManagementSystemX.Database.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly Context _context;

        public UnitOfWork()
        {
            _context = new Context();
        }


        private ProductRepository _productRepository = null!;
        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_context);
                }
                return _productRepository;
            }
        }

        private UserRepository _userRepository = null!;
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }

        private TransactionRepository _transactionRepository = null!;
        public ITransactionRepository TransactionRepository
        {
            get
            {
                if (_transactionRepository == null)
                {
                    _transactionRepository = new TransactionRepository(_context);
                }
                return _transactionRepository;
            }
        }

        private TransactionProductRepository _transactionProductRepository = null!;
        public ITransactionProductRepository TransactionProductRepository
        {
            get
            {
                if (_transactionProductRepository == null)
                {
                    _transactionProductRepository = new TransactionProductRepository(_context);
                }
                return _transactionProductRepository;
            }
        }

        private StockPurchaseRepository _stockPurchaseRepository = null!;
        public IStockPurchaseRepository StockPurchaseRepository
        {
            get
            {
                if (_stockPurchaseRepository == null)
                {
                    _stockPurchaseRepository = new StockPurchaseRepository(_context);
                }
                return _stockPurchaseRepository;
            }
        }

        private PayLaterRepository _payLaterRepository = null!;
        public IPayLaterRepository PayLaterRepository
        {
            get
            {
                if (_payLaterRepository == null)
                {
                    _payLaterRepository = new PayLaterRepository(_context);
                }
                return _payLaterRepository;
            }
        }

        private StockPurchaseProductRepository _stockPurchaseProductRepository = null!;
        public IStockPurchaseProductRepository StockPurchaseProductRepository
        {
            get
            {
                if (_stockPurchaseProductRepository == null)
                {
                    _stockPurchaseProductRepository = new StockPurchaseProductRepository(_context);
                }
                return _stockPurchaseProductRepository;
            }
        }

        public void Save()
        {
            Console.WriteLine("-----------------------------Change Tracking Debug View  ---------------------------------");
            Console.WriteLine("--------------------------------------------------------------------------");
            _context.ChangeTracker.DetectChanges();
            Console.WriteLine(_context.ChangeTracker.DebugView.LongView);
            _context.SaveChanges();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                    Console.WriteLine("Disposing...");
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            Console.WriteLine("Disposed");
        }

        public void Attach(object entity)
        {
            _context.Attach(entity);
        }

        public void De(object entity)
        {
            _context.Attach(entity);
        }
    }
}
