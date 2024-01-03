using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Factories.Transactions.Interfaces
{
    public interface IPayLaterReconstitutionArgs
    {
        public string CustomerName { get; }

        public bool IsPaid { get; }

        public DateTime? PaidAt { get; }
    }
}
