using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.Transactions.Interfaces
{
    public interface ITransactionListViewModel
    {
        public ObservableCollection<ITransactionRowViewModel> Transactions { get; }

        public ITransactionRowViewModel? SelectedTransaction { get; set; }

        public ICommand NewTransactionCommand { get; }
    }
}
