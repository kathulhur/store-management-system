using StoreManagementSystemX.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Infrastructure.Models
{
    public class StockPurchaseProductDBModel
    {
        public Guid StockPurchaseId { get; set; }

        public StockPurchaseDBModel StockPurchase { get; set; } = null!;

        public Guid ProductId { get; set;  }

        public ProductDBModel Product { get; set; } = null!;

        public string Barcode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int QuantityBought { get; set; }
    }
}
