using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Aggregates.Roots.Transactions
{
    public class PayLater : IPayLater
    {
        public PayLater(string customerName)
        {
            CustomerName = customerName;
            IsPaid = false;
        }

        public string CustomerName { get; set; } = String.Empty;

        public bool IsPaid { get; internal set; }

        public DateTime? PaidAt { get; set; }

    }
}
