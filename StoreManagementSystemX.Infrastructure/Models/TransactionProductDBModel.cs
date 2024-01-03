using StoreManagementSystemX.Domain.Factories.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Infrastructure.DTO
{
    [Table("TransactionProducts")]
    public class TransactionProductDBModel
    {
        public Guid TransactionId { get; set; }

        public TransactionDBModel Transaction { get; set; } = null!;

        public Guid ProductId { get; set; }

        public ProductDBModel Product { get; set; } = null!;

        public string ProductName { get; set; } = string.Empty;

        public decimal CostPrice { get; set; }

        public decimal SellingPrice { get; set; }

        public int QuantityBought { get; set; }
    }
}
