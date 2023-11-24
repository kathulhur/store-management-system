using System.Collections.Generic;
using System;
using System.Windows.Input;
using StoreManagementSystemX.Views.Transactions;

namespace StoreManagementSystemX.ViewModels.Transactions.Interfaces
{
    public interface ITransactionRowViewModel
    {
        public Guid Id { get; }

        public string CustomerName { get; }

        public DateTime DateTime { get; }

        public IEnumerable<ITransactionProductRowViewModel> TransactionProducts { get; }

        public event EventHandler<EventArgs<ITransactionRowViewModel>> TransactionDeleted;

        public bool IsPayLater { get; }

        public decimal TotalCost { get; }

        public decimal TotalPrice { get; }

        public ICommand DeleteCommand { get; }



    }

}