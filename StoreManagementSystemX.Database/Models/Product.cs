using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Database.Models
{
    public class Product
    {

        public Product()
        {

        }

        public Guid Id { get; set; }

        public string Barcode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public decimal CostPrice { get; set; }

        public decimal SellingPrice { get; set; }

        public int InStock { get; set; }

        public ICollection<TransactionProduct> SoldStocks { get; } = new List<TransactionProduct>();

        public ICollection<StockPurchase> StockPurchases { get; } = new List<StockPurchase>();

        public ICollection<Transaction> Transactions { get; } = new List<Transaction>();

        public ICollection<StockPurchaseProduct> StockPurchaseProducts { get; } = new List<StockPurchaseProduct>();

        public Guid CreatedById { get; set; }

        public User CreatedBy { get; set; } = null!;

        public override string ToString()
        {
            return $"ID: {Id}, Barcode: {Barcode}, Product Name: {Name}, Created By: {CreatedBy?.Username}, Cost Price: {CostPrice}, Selling Price: {SellingPrice}";
        }
    }
}
