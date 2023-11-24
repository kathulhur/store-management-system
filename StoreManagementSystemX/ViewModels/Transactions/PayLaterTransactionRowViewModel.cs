using CommunityToolkit.Mvvm.Input;
using SQLitePCL;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using StoreManagementSystemX.Services.Interfaces;
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
        public PayLaterTransactionRowViewModel(IUnitOfWorkFactory unitOfWorkFactory, Transaction transaction, IDialogService dialogService)
        {
            _transaction = transaction;
            _unitOfWorkFactory = unitOfWorkFactory;
            _dialogService = dialogService;
            _markAsPaidCommand = new RelayCommand(MarkAsPaid, CanMarkAsPaid);
            foreach (var transactionProduct in transaction.TransactionProducts)
            {
                _transactionProducts.Add(new TransactionProductRowViewModel(transactionProduct));
            }
        }

        private readonly Transaction _transaction;

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

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

                    using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
                    {
                        unitOfWork.Attach(_transaction.PayLater);
                        _transaction.PayLater.PaidAt = DateTime.Now;
                        _transaction.PayLater.IsPaid = true;
                        unitOfWork.Save();

                    }
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
