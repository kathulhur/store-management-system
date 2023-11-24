using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.Transactions.Interfaces
{
    public interface ICreateTransactionViewModel
    {

        public string Barcode { get; set; }

        public string CustomerName { get; set; }

        public ObservableCollection<ICreateTransactionProductViewModel> TransactionProducts { get; }

        public bool IsPayLater { get; set; }

        public decimal TotalAmount { get; }

        public ICommand DoneCommand { get; }

        public ICommand CancelCommand { get; }

    }
}
