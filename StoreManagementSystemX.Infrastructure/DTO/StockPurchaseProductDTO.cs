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
    public class StockPurchaseProductDTO : IStockPurchaseProductReconstitutionArgs
    {
        public StockPurchaseProductDTO(StockPurchaseProductDBModel stockPurchaseProduct)
        {
            ProductId = stockPurchaseProduct.ProductId;
            Barcode = stockPurchaseProduct.Barcode;
            Name = stockPurchaseProduct.Name;
            Price = stockPurchaseProduct.Price;
            QuantityBought = stockPurchaseProduct.QuantityBought;
        }

        public StockPurchaseProductDTO(IStockPurchaseProduct stockPurchaseProduct)
        {
            ProductId = stockPurchaseProduct.ProductId;
            Barcode = stockPurchaseProduct.Barcode;
            Name = stockPurchaseProduct.Name;
            Price = stockPurchaseProduct.Price;
            QuantityBought = stockPurchaseProduct.QuantityBought;
        }

        public Guid ProductId { get; }

        public string Barcode { get; }

        public string Name { get; }

        public decimal Price { get; }

        public int QuantityBought { get; }
    }
}
