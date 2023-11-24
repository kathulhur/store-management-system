using Microsoft.EntityFrameworkCore;
using StoreManagementSystemX.Database.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Database.DAL
{
    public class TransactionRepository : ITransactionRepository, IDisposable
    {

        public TransactionRepository(Context context)
        {
            _context = context;
        }

        private readonly Context _context;

        public void Delete(Guid instanceId)
        {
            Transaction transaction = _context.Transactions.Find(instanceId);
            _context.Transactions.Remove(transaction);
        }


        public IEnumerable<Transaction> GetAll()
        {
            return _context.Transactions.Include(t => t.TransactionProducts).Include(e => e.PayLater).ToList();
        }

        public Transaction? GetById(Guid transactionId)
        {
            return _context.Transactions.Include(t => t.TransactionProducts).FirstOrDefault(t => t.Id == transactionId);
        }

        public void Insert(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
        }

        public void Save()
        {
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
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Transaction> Find(Expression<Func<Transaction, bool>> predicate)
        {
            return _context.Transactions.Where(predicate).Include(t => t.TransactionProducts).Include(e => e.PayLater).ToList();
        }

    }
}
