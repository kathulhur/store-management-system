using StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases;
using StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases.Interfaces;
using StoreManagementSystemX.Domain.Factories.StockPurchases.Interfaces;
using StoreManagementSystemX.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Infrastructure.DTO
{
    public class StockPurchaseDTO : IStockPurchaseReconstitutionArgs
    {
        public StockPurchaseDTO(StockPurchaseDBModel stockPurchase, IList<StockPurchaseProductDTO> stockPurchaseProducts)
        {
            StockManagerId = stockPurchase.StockManagerId;
            Id = stockPurchase.Id;
            DateTime = stockPurchase.DateTime;
            StockPurchaseProducts = stockPurchaseProducts;
        }

        public StockPurchaseDTO(IStockPurchase stockPurchase, IEnumerable<StockPurchaseProductDTO> stockPurchaseProducts)
        {
            StockManagerId = stockPurchase.StockManagerId;
            Id = stockPurchase.Id;
            DateTime = stockPurchase.DateTime;
            StockPurchaseProducts = stockPurchaseProducts;
        }

        public Guid StockManagerId { get; }

        public Guid Id { get; }

        public DateTime DateTime { get; }

        public IEnumerable<IStockPurchaseProductReconstitutionArgs> StockPurchaseProducts { get;  }
    }
}
