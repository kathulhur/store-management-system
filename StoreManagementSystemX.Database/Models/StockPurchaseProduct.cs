using StoreManagementSystemX.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StoreManagementSystemX.Database.Models
{
    public class StockPurchaseProduct
    {

        public Guid StockPurchaseId { get; set; }

        public StockPurchase StockPurchase { get; set; } = null!;

        public Guid ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public decimal Price { get; set; }

        public int QuantityBought { get; set; }

        public decimal TotalPrice => Price * QuantityBought;

        public override string ToString()
        {
            return $"StockPurchaseId: {StockPurchaseId}, ProductId: {ProductId}, Price: {Price}, Quantity Bought: {QuantityBought}";
        }
    }
}
