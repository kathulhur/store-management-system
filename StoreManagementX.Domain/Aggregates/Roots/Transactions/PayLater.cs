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
        internal PayLater(string customerName)
        {
            CustomerName = customerName;
            IsPaid = false;
        }

        internal PayLater(string customerName, bool isPaid, DateTime? paidAt)
        {
            CustomerName = customerName;
            IsPaid = isPaid;
            PaidAt = paidAt;
        }

        public string CustomerName { get; internal set; } = String.Empty;

        public bool IsPaid { get; internal set; }

        public DateTime? PaidAt { get; internal set; }

    }
}
