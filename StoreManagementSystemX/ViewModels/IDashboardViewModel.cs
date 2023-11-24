using StoreManagementSystemX.ViewModels.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.Interfaces
{
    public interface IDashboardViewModel
    {
        public DateTime DateToday { get; }

        public ObservableCollection<ITransactionRowViewModel> TransactionsToday { get; }

        public int TotalTransactionsToday { get; }

        public decimal TotalRevenueToday { get; }

        public decimal TotalProfitToday { get; }

        public ITransactionRowViewModel? SelectedTransaction { get; }

        public string TotalPurchased { get; }

        public ICommand NewTransactionCommand { get; }

    }
}
