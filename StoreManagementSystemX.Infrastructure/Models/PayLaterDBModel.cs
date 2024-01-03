using Microsoft.EntityFrameworkCore;
using StoreManagementSystemX.Domain.Factories.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Infrastructure.DTO
{
    [Owned]
    public class PayLaterDBModel
    {
        public string CustomerName { get; set; } = string.Empty;

        public bool IsPaid { get; set; }

        public DateTime? PaidAt { get; set; }
    }
}
