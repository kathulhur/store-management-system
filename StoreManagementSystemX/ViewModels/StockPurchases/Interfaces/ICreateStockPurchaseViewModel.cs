using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.StockPurchases.Interfaces
{
    public interface ICreateStockPurchaseViewModel
    {
        public string Barcode { get; set; }

        public decimal TotalAmount { get; }

        public ObservableCollection<ICreateStockPurchaseProductViewModel> StockPurchaseProducts { get; }

        public ICommand CancelCommand { get; }

        public ICommand DoneCommand { get; }

    }
}
