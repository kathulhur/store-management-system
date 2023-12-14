using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Factories.Transactions.Interfaces
{
    public interface IPayLaterFactory
    {
        public IPayLater Create(string customerName);
    }
}
