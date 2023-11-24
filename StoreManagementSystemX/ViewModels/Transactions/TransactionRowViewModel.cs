using CommunityToolkit.Mvvm.Input;
using SQLitePCL;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.ViewModels.Transactions;
using StoreManagementSystemX.ViewModels.Transactions.Interfaces;
using StoreManagementSystemX.Views.Transactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.Transactions
{
    public class TransactionRowViewModel : ITransactionRowViewModel
    {
        public TransactionRowViewModel(IUnitOfWorkFactory unitOfWorkFactory, IDialogService dialogService, Transaction transaction)
        {
            _transaction = transaction;
            _transactionProducts = new List<TransactionProductRowViewModel>();
            foreach (var t in _transaction.TransactionProducts)
            {
                _transactionProducts.Add(new TransactionProductRowViewModel(t));
            }
            _unitOfWorkFactory = unitOfWorkFactory;
            _dialogService = dialogService;
            DeleteCommand = new RelayCommand(DeleteCommandHandler);
        }

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IDialogService _dialogService;
        private readonly Transaction _transaction;

        public Guid Id => _transaction.Id;

        public string CustomerName => _transaction?.PayLater?.CustomerName ?? "";

        public DateTime DateTime => _transaction.DateTime;

        private readonly List<TransactionProductRowViewModel> _transactionProducts;
        public IEnumerable<ITransactionProductRowViewModel> TransactionProducts => _transactionProducts;


        public bool IsPayLater => _transaction.PayLater != null;

        public decimal TotalPrice => TransactionProducts.Sum(e => e.TotalPrice);

        public event EventHandler<EventArgs<ITransactionRowViewModel>> TransactionDeleted = null!;

        public ICommand DeleteCommand { get; }

        public decimal TotalCost => TransactionProducts.Sum(tp => tp.TotalCost);

        private void OnTransactionDeleted(EventArgs<ITransactionRowViewModel> e)
        {
            TransactionDeleted?.Invoke(this, e);
        }

        private void DeleteCommandHandler()
        {
            if(_dialogService.ShowConfirmationDialog("Confirm Delete", "Do you really want to delete this transaction record?"))
            {
                using(var unitOfWork =  _unitOfWorkFactory.CreateUnitOfWork())
                {
                    unitOfWork.TransactionRepository.Delete(Id);
                    unitOfWork.Save();
                    OnTransactionDeleted(new EventArgs<ITransactionRowViewModel>(this));
                };
            }
        }
       


    }
}
