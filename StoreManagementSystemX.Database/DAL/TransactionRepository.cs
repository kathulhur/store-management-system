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
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {

        public TransactionRepository(Context context) : base(context) { }

        public Transaction? GetById(Guid transactionId, string includeProperties = "")
        {
            IQueryable<Transaction> query = _dbSet;

            foreach(var property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                query = query.Include(property);
            }

            var result = query.Where(t => t.Id == transactionId).FirstOrDefault();
            return result;
        }
    }
}
