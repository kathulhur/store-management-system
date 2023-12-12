using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;

namespace StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases.Interfaces
{
    // Aggregate

    // Aggregate root
    public interface IStockPurchase
    {
        public Guid Id { get; }

        public Guid StockManagerId { get; }

        public IReadOnlyList<IStockPurchaseProduct> StockPurchaseProducts { get; }

        public decimal TotalAmount { get; }

        public void AddProduct(IProduct product);

        public void RemoveProduct(IProduct product);


        
        
    }
}
