using StoreManagementSystemX.Domain.Aggregates.Roots.Products;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;
using StoreManagementSystemX.Domain.Factories.Products.Interfaces;
using StoreManagementSystemX.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Infrastructure.DTO
{
    public class ProductDTO : IProductReconstitutionArgs
    {
        public ProductDTO(IProduct product)
        {
            Id = product.Id;
            Barcode = product.Barcode;
            Name = product.Name;
            CostPrice = product.CostPrice;
            SellingPrice = product.SellingPrice;
            InStock = product.InStock;
            CreatorId = product.CreatorId;
        }

        public ProductDTO(ProductDBModel product)
        {
            Id = product.Id;
            Barcode = product.Barcode;
            Name = product.Name;
            CostPrice = product.CostPrice;
            SellingPrice = product.SellingPrice;
            InStock = product.InStock;
            CreatorId = product.CreatorId;
        }

        public Guid Id { get; set; }

        public string Barcode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public decimal CostPrice { get; set; }

        public decimal SellingPrice { get; set; }

        public int InStock { get; set; }

        public Guid CreatorId { get; set; }

        public ProductDBModel ToDBModel()
        {
            return new ProductDBModel
            {
                Id = Id,
                Barcode = Barcode,
                Name = Name,
                CostPrice = CostPrice,
                SellingPrice = SellingPrice,
                InStock = InStock,
                CreatorId = CreatorId,
            };
        }
    }
}
