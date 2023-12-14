using CommunityToolkit.Mvvm.Input;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Transactions.Interfaces;
using StoreManagementSystemX.Services;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.ViewModels;
using StoreManagementSystemX.ViewModels.Transactions;
using StoreManagementSystemX.ViewModels.Transactions.Interfaces;
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
    public class PayLaterTransactionsViewModel: BaseViewModel
    {
        public PayLaterTransactionsViewModel(AuthContext authContext, Domain.Repositories.Transactions.Interfaces.ITransactionRepository transactionRepository, IDialogService dialogService, ITransactionCreationService transactionCreationService)
        {
            _transactionRepository = transactionRepository;
            _authContext = authContext;
            _dialogService = dialogService;
            _transactionCreationService = transactionCreationService;
            Transactions = new ObservableCollection<PayLaterTransactionRowViewModel>();
            NewTransactionCommand = new RelayCommand(NewTransactionCommandHandler);

            foreach(var transaction in _transactionRepository.GetAll())
            {
                if(transaction != null)
                {
                    Transactions.Add(new PayLaterTransactionRowViewModel(transactionRepository, transaction, dialogService));
                }
            }

            if (Transactions.Any())
            {
                SelectedTransaction = Transactions.First();
            }
        }

        private readonly Domain.Repositories.Transactions.Interfaces.ITransactionRepository _transactionRepository;
        private readonly AuthContext _authContext;
        private readonly IDialogService _dialogService;
        private readonly ITransactionCreationService _transactionCreationService;

        public ICommand NewTransactionCommand { get; }

        private void NewTransactionCommandHandler()
        {
            var newTransactionId = _transactionCreationService.CreateNewTransaction(_authContext);
            if (newTransactionId != null)
            {
                var newTransaction = _transactionRepository.GetById((Guid)newTransactionId);
                if(newTransaction != null && newTransaction.PayLater != null)
                {
                    var newTransactionRow = new PayLaterTransactionRowViewModel(_transactionRepository, newTransaction, _dialogService);
                    //SubscribeToTransactionRowEvents(newTransactionRow);
                    Transactions.Insert(0, newTransactionRow);
                }

            }
        }

        public ObservableCollection<PayLaterTransactionRowViewModel> Transactions { get; }


        public PayLaterTransactionRowViewModel? SelectedTransaction { get; set; }

        public void OnCreateTransaction(ITransaction newTransaction)
        {
            if(newTransaction.PayLater != null) 
            {
                Transactions?.Insert(0, new PayLaterTransactionRowViewModel(_transactionRepository, newTransaction, _dialogService));
            }

        }



    }
}
