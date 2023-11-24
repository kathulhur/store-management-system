using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Database.DAL
{
    public class TransactionProductRepository : ITransactionProductRepository, IDisposable
    {
        public TransactionProductRepository(Context context)
        {
            _context = context;
        }

        private readonly Context _context;

        public void Delete(Guid transactionProductId)
        {
            throw new NotImplementedException();
        }


        public void Delete(Guid transactionId, Guid productId)
        {
            TransactionProduct transactionProduct = _context.TransactionProducts.Find(transactionId, productId);
            _context.TransactionProducts.Remove(transactionProduct);
        }

        public IEnumerable<TransactionProduct> GetAll()
        {
            return _context.TransactionProducts.ToList();
        }

        public TransactionProduct? GetById(Guid instanceId)
        {
            throw new NotImplementedException();
        }

        public TransactionProduct? GetById(Guid transactionId, Guid productId)
        {
            return _context.TransactionProducts.Find(transactionId, productId);
        }

        public void Insert(TransactionProduct transactionProduct)
        {
            _context.TransactionProducts.Add(transactionProduct);
        }

        public void Save()
        {
            _context.SaveChanges();
        }


        private bool disposed;

        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
