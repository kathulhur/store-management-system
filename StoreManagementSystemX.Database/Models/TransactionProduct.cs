using StoreManagementSystemX.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Database.Models
{
    public class TransactionProduct
    {


        public Guid TransactionId { get; set; }

        public Transaction Transaction { get; set; } = null!;

        public Guid ProductId {  get; set; } 

        public Product Product { get; set; } = null!;

        public string ProductName { get; set; }

        public int QuantityBought { get; set; }

        public decimal PriceSold { get; set; }

        public decimal CostPrice { get; set; }


        public override string ToString()
        {
            return $"TransactionId: {Transaction.Id}, Product Name: {Product.Name}, Cost Price: {CostPrice}, Price Sold: {PriceSold}, Quantity {QuantityBought}";
        }
    }
}
