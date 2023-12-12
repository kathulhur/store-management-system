using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;
using StoreManagementSystemX.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Aggregates.Roots.Products
{
    public class Product : IProduct
    {
        internal Product(Guid creatorId, Guid id, string name, decimal costPrice, decimal sellingPrice, int inStock = 0)
        {
            Id = id;
            Name = name;
            CostPrice = costPrice;
            SellingPrice = sellingPrice;
            InStock = inStock;
            CreatorId = creatorId;
        }

        public Guid CreatorId { get; }

        public Guid Id { get; }

        public string Name { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SellingPrice { get; set; }

        public int InStock { get; set; }

        public override string ToString()
            => $"ID: {Id} - Name: {Name} - Cost Price: {CostPrice} - SellingPrice: {SellingPrice} - In Stock: {InStock}";

        public override bool Equals(object? obj)
        {
            return obj is Product product &&
                   Id.Equals(product.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
