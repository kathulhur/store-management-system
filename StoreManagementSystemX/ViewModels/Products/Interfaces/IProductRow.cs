using StoreManagementSystemX.ViewModels.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.Products.Interfaces
{
    public interface IProductRow
    {
        public Guid Id { get; }
        public string Barcode { get; }
        public string Name { get; }
        public int InStock { get; }
        public decimal CostPrice { get; }
        public decimal SellingPrice { get; }

        public ICommand UpdateCommand { get; }

        public event EventHandler<EventArgs<IProductRow>> ProductDeleted;
        public ICommand DeleteCommand { get; }
    }
}
