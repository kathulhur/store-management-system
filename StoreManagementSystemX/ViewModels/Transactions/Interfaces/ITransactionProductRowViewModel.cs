using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Views.Transactions
{
    public interface ITransactionProductRowViewModel
    {
        public string ProductName { get; }

        public int Quantity { get; }

        public decimal SellingPrice { get; }

        public decimal TotalCost { get; }

        public decimal TotalPrice { get; }
    }
}
