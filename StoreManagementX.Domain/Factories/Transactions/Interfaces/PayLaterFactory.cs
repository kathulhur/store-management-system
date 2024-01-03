using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions;
using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Factories.Transactions.Interfaces
{
    public class PayLaterFactory : IPayLaterFactory
    {
        public PayLaterFactory()
        {

        }

        public IPayLater Create(string customerName)
        {
            return new PayLater(customerName);
        }

        public IPayLater Reconstitute(IPayLaterReconstitutionArgs args)
        {
            return new PayLater(
                args.CustomerName,
                args.IsPaid,
                args.PaidAt
            );
        }
    }
}
