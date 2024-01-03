using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces;
using StoreManagementSystemX.Domain.Factories.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Infrastructure.DTO
{
    public class PayLaterDTO : IPayLaterReconstitutionArgs
    {
        public PayLaterDTO(IPayLater payLater)
        {
            CustomerName = payLater.CustomerName;
            IsPaid = payLater.IsPaid;
            PaidAt = payLater.PaidAt;
        }

        public PayLaterDTO(PayLaterDBModel payLater)
        {
            CustomerName = payLater.CustomerName;
            IsPaid = payLater.IsPaid;
            PaidAt = payLater.PaidAt;
        }

        public string CustomerName { get; set; }

        public bool IsPaid { get; set; }

        public DateTime? PaidAt { get; set; }

        public PayLaterDBModel ToDBModel()
        {
            return new PayLaterDBModel
            {
                CustomerName = CustomerName,
                IsPaid = IsPaid,
                PaidAt = PaidAt
            };
        }
    }
}
