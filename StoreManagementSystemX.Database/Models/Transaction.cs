using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace StoreManagementSystemX.Database.Models
{
    public class Transaction
    {

        public Transaction() { }

        public Guid Id { get; set; }

        // Don't work in sqlite
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateTime { get; set; }

        public ICollection<TransactionProduct> TransactionProducts { get; } = new List<TransactionProduct>();

        public ICollection<Product> Products { get; } = new List<Product>();

        public PayLater? PayLater { get; set; }

        public Guid SellerId { get; set; }

        public User Seller { get; set; } = null!;

        public decimal TotalAmount => TransactionProducts.Sum(t => t.PriceSold * t.QuantityBought);

        public override string ToString()
        {
            return $"ID: {Id}, Date & Time: {DateTime}, PayLater?: {PayLater}, Seller: {Seller.Username}";
        }
    }
}
