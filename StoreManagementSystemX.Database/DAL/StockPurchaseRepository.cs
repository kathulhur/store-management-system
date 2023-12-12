using Microsoft.EntityFrameworkCore;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Database.DAL
{
    public class StockPurchaseRepository : BaseRepository<StockPurchase>, IStockPurchaseRepository
    {

        public StockPurchaseRepository(Context context) : base(context) { }

        public StockPurchase? GetById(Guid stockPurchaseId, string includeProperties = "")
        {
            IQueryable<StockPurchase> query = _dbSet.Where(t => t.Id == stockPurchaseId);

            foreach (var property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                query = query.Include(property);
            }

            var entityFound = query.FirstOrDefault();
            return entityFound;
        }
    }
}
