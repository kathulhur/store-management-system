using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Database.DAL
{
    public interface ITransactionRepository: IRepository<Transaction>
    {
        public Transaction? GetById(Guid transactionId, string includeProperties = "");
    }
}
