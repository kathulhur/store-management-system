using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Database.DAL
{
    public class TransactionProductRepository : BaseRepository<TransactionProduct>, ITransactionProductRepository
    {
        public TransactionProductRepository(Context context) : base(context) { }
    }
}
