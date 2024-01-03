using StoreManagementSystemX.Domain.Factories.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Infrastructure.DTO
{
    public class TransactionProductDTO : ITransactionProductReconstitutionArgs
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SellingPrice { get; set; }

        public int QuantityBought { get; set; }
    }
}
