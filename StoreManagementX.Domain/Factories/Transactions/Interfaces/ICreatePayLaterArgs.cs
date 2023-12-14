using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Factories.Transactions.Interfaces
{
    public interface ICreatePayLaterArgs
    {
        public string CustomerName { get; set; }
    }
}
