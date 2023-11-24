using StoreManagementSystemX.ViewModels.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.Interfaces
{
    public interface IInventoryViewModel
    {
        public ObservableCollection<IProductRow> Products { get; }

        public ICommand AddProductCommand { get; }
    }
}
