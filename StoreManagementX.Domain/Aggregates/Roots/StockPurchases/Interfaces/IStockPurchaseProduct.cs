using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases.Interfaces
{
    public interface IStockPurchaseProduct
    {
            public Guid ProductId { get; }

            public string ProductName { get; }

            public decimal CostPrice { get; }

            public int QuantityBought { get; }

            public decimal TotalCost { get; }
    }
}
