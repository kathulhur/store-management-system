using StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases;
using StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases.Interfaces;
using StoreManagementSystemX.Domain.Factories.StockPurchases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases.StockPurchase;

namespace StoreManagementSystemX.Domain.Factories.StockPurchases
{
    public class StockPurchaseFactory : IStockPurchaseFactory
    {
        public IStockPurchase Create(Guid stockManagerId)
        {
            return new StockPurchase(stockManagerId, Guid.NewGuid());
        }

        public IStockPurchase Reconstitute(IStockPurchaseReconstitutionArgs args)
        {
            var stockPurchaseProducts = new List<StockPurchaseProduct>();
            foreach(var stockPurchaseProductDTO in args.StockPurchaseProducts)
            {
                stockPurchaseProducts.Add(new StockPurchaseProduct(
                    stockPurchaseProductDTO.ProductId,
                    stockPurchaseProductDTO.Barcode,
                    stockPurchaseProductDTO.Name,
                    stockPurchaseProductDTO.Price,
                    stockPurchaseProductDTO.QuantityBought
                ));
            }
            return new StockPurchase(
                args.StockManagerId,
                args.Id,
                args.DateTime,
                args.StockPurchaseProducts.Sum(e => e.Price * e.QuantityBought),
                stockPurchaseProducts
            );
        }
    }
}
