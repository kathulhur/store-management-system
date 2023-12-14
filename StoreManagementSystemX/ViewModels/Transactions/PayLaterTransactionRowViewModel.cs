using CommunityToolkit.Mvvm.Input;
using SQLitePCL;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Transactions.Interfaces;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.ViewModels.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.Transactions
{
    public class PayLaterTransactionRowViewModel : BaseViewModel
    {
        public PayLaterTransactionRowViewModel(
            Domain.Repositories.Transactions.Interfaces.ITransactionRepository transactionRepository, 
            ITransaction transaction, 
            IDialogService dialogService)
        {
            _transaction = transaction;
            _transactionRepository = transactionRepository;
            _dialogService = dialogService;
            _markAsPaidCommand = new RelayCommand(MarkAsPaid, CanMarkAsPaid);
            foreach (var transactionProduct in transaction.TransactionProducts)
            {
                _transactionProducts.Add(new TransactionProductRowViewModel(transactionProduct));
            }
        }

        private readonly ITransaction _transaction;

        private readonly Domain.Repositories.Transactions.Interfaces.ITransactionRepository _transactionRepository;

        private readonly IDialogService _dialogService;

        public DateTime DateTime => _transaction.DateTime;

        public decimal TotalSales => TransactionProducts.Sum(e => e.TotalPrice);

        public string CustomerName => _transaction?.PayLater?.CustomerName ?? "";

        private readonly RelayCommand _markAsPaidCommand;

        public ICommand MarkAsPaidCommand => _markAsPaidCommand;

        private void MarkAsPaid()
        {
            bool ok = _dialogService.ShowConfirmationDialog(
                "Confirm payment", $"Are you sure you want to mark this transaction as paid? \n\n {CustomerName} : ₱{TotalSales}");
            if (ok)
            {
                if (_transaction.PayLater != null)
                {
                    _transaction.MarkAsPaid();
                    _transactionRepository.Update(_transaction);
                }
                _markAsPaidCommand.NotifyCanExecuteChanged();
            }
        }

        private bool CanMarkAsPaid()
            => _transaction.PayLater != null && !_transaction.PayLater.IsPaid;


        private readonly List<TransactionProductRowViewModel> _transactionProducts = new List<TransactionProductRowViewModel>();
        public IEnumerable<TransactionProductRowViewModel> TransactionProducts => _transactionProducts;

    }
}
