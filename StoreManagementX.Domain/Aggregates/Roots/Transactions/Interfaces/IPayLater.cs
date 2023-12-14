using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces
{
    public interface IPayLater
    {
        public string CustomerName { get; }

        public bool IsPaid { get; }

        public DateTime? PaidAt { get; }
    }
}
