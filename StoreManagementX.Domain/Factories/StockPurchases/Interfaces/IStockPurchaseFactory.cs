using StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Factories.StockPurchases.Interfaces
{
    public interface IStockPurchaseFactory
    {
        public IStockPurchase Create(Guid stockManagerId);
    }
}
