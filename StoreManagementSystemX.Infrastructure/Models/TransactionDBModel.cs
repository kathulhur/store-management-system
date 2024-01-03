using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces;
using StoreManagementSystemX.Domain.Factories.Transactions.Interfaces;
using StoreManagementSystemX.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Infrastructure.DTO
{
    public class TransactionDBModel
    {
        public Guid Id { get; set; }

        public Guid SellerId { get; set; }

        public UserDBModel Seller { get; set; } = null!;

        public DateTime DateTime { get; set; }

        public PayLaterDBModel? PayLater { get; set; }

        public List<TransactionProductDBModel> TransactionProducts { get; set; } = new List<TransactionProductDBModel>();

        public List<ProductDBModel> Products { get; } = new List<ProductDBModel>();
    }
}
