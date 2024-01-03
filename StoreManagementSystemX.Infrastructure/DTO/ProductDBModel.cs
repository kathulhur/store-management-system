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
    public class ProductDBModel : IProductReconstitutionArgs
    {
        public ProductDBModel() { }

        public Guid Id { get; set; }

        public string Barcode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public decimal CostPrice { get; set; }

        public decimal SellingPrice { get; set; }

        public int InStock { get; set; }

        public UserDBModel Creator { get; set; } = null!;

        public Guid CreatorId { get; set; }

        public List<TransactionProductDBModel> TransactionProducts { get; set; } = new List<TransactionProductDBModel>();

        public List<TransactionDBModel> Transactions { get; set; } = new List<TransactionDBModel>();

        public List<StockPurchaseDBModel> StockPurchases { get; set; } = new List<StockPurchaseDBModel>();
    }
}
