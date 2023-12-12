using StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases;
using StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases.Interfaces;
using StoreManagementSystemX.Domain.Factories.StockPurchases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Factories.StockPurchases
{
    public class StockPurchaseFactory : IStockPurchaseFactory
    {
        public IStockPurchase Create(Guid stockManagerId)
        {
            return new StockPurchase(stockManagerId, Guid.NewGuid());
        }
    }
}
