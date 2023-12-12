using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces
{
    public interface ITransactionProduct
    {
            public Guid ProductId { get; }

            public string ProductName { get; }

            public decimal CostPrice { get; }

            public decimal SellingPrice { get; }

            public int QuantityBought { get; }

            public decimal TotalPrice { get; }
    }
}
