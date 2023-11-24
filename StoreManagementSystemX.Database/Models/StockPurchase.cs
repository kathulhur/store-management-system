using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StoreManagementSystemX.Database.Models
{
    public class StockPurchase
    {
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateTime{ get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();

        public ICollection<StockPurchaseProduct> StockPurchaseProducts { get; set; } = new List<StockPurchaseProduct>();

        public Guid MadeById { get; set; }

        public User MadeBy { get; set; } = null!;

        public decimal TotalCost => StockPurchaseProducts.Sum(e => e.TotalPrice);

        public override string ToString()
        {
            return $"ID: {Id}, Barcode: {DateTime}, MadeById: {MadeById}";
        }
    }


}

