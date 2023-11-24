using CommunityToolkit.Mvvm.Input;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using StoreManagementSystemX.Services;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.ViewModels;
using StoreManagementSystemX.ViewModels.Transactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.Transactions
{
    class PayLaterTransactionsViewModel: BaseViewModel
    {
        public PayLaterTransactionsViewModel(AuthContext authContext, IUnitOfWorkFactory unitOfWorkFactory, IDialogService dialogService, ITransactionCreationService transactionCreationService)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _authContext = authContext;
            _dialogService = dialogService;
            _transactionCreationService = transactionCreationService;
            Transactions = new ObservableCollection<PayLaterTransactionRowViewModel>();

            using(var unitOfWork  = _unitOfWorkFactory.CreateUnitOfWork())
            {
                foreach(var transaction in unitOfWork.TransactionRepository.Find(t => t.PayLater != null).OrderByDescending(t => t.DateTime))
                {
                    if(transaction != null)
                    {
                        Transactions.Add(new PayLaterTransactionRowViewModel(unitOfWorkFactory, transaction, dialogService));
                    }
                }

            }

            if (Transactions.Any())
            {
                SelectedTransaction = Transactions.First();
            }
        }

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly AuthContext _authContext;
        private readonly IDialogService _dialogService;
        private readonly ITransactionCreationService _transactionCreationService;

        public ICommand NewTransactionCommand { get; }

        private void NewTransactionCommandHandler()
        {
            var newTransactionId = _transactionCreationService.CreateNewTransaction(_authContext);
            if (newTransactionId != null)
            {
                using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
                {
                    var newTransaction = unitOfWork.TransactionRepository.GetById((Guid)newTransactionId);
                    if(newTransaction != null)
                    {
                        var newTransactionRow = new PayLaterTransactionRowViewModel(_unitOfWorkFactory, newTransaction, _dialogService);
                        //SubscribeToTransactionRowEvents(newTransactionRow);
                        Transactions.Insert(0, newTransactionRow);
                    }
                }

            }
        }

        //private void SubscribeToTransactionRowEvents(PayLaterTransactionRowViewModel transactionRow)
        //{
        //    transactionRow.TransactionDeleted += HandleTransactionDeletion;
        //}

        //private void UnsubscribeToTransactionRowEvents(ITransactionRowViewModel transactionRow)
        //{
        //    transactionRow.TransactionDeleted -= PayLaterTransactionRowViewModel;
        //}

        public ObservableCollection<PayLaterTransactionRowViewModel> Transactions { get; }


        public PayLaterTransactionRowViewModel? SelectedTransaction { get; set; }

        public void OnCreateTransaction(Transaction newTransaction)
        {
            if(newTransaction.PayLater != null) 
            {
                Transactions?.Insert(0, new PayLaterTransactionRowViewModel(_unitOfWorkFactory, newTransaction, _dialogService));
            }

        }



    }
}
