using StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Repositories.StockPurchases.Interfaces
{
    public interface IStockPurchaseRepository : IRepository<IStockPurchase>
    {
    }
}
