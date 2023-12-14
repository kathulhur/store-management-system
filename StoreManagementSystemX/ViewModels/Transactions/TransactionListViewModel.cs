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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.Transactions
{
    public class TransactionListViewModel : BaseViewModel, ITransactionListViewModel
    {
        public TransactionListViewModel(AuthContext authContext, Domain.Repositories.Transactions.Interfaces.ITransactionRepository transactionRepository, IDialogService dialogService, ITransactionCreationService transactionCreationService)
        {
            _transactionRepository = transactionRepository;
            _authContext = authContext;
            _dialogService = dialogService;
            _transactionCreationService = transactionCreationService;
            Transactions = new ObservableCollection<ITransactionRowViewModel>();

            foreach (var transaction in _transactionRepository.GetAll())
            {
                AddTransaction(transaction);
            }


            if (Transactions.Count() > 0)
            {
                SelectedTransaction = Transactions.First();
            }
            NewTransactionCommand = new RelayCommand(NewTransactionCommandHandler);
        }

        private Domain.Repositories.Transactions.Interfaces.ITransactionRepository _transactionRepository { get; }
        private AuthContext _authContext { get; }
        private IDialogService _dialogService { get; }
        private ITransactionCreationService _transactionCreationService { get; }

        public ObservableCollection<ITransactionRowViewModel> Transactions { get; }

        private ITransactionRowViewModel? _selectedTransaction;
        public ITransactionRowViewModel? SelectedTransaction
        {
            get
            {

                return _selectedTransaction ?? null;
            }
            set
            {
                SetProperty(ref _selectedTransaction, value);
            }
        }

        public ICommand NewTransactionCommand { get; }

        private void NewTransactionCommandHandler()
        {
            var newTransactionId = _transactionCreationService.CreateNewTransaction(_authContext);
            if (newTransactionId != null)
            {
                var newTransaction = _transactionRepository.GetById((Guid)newTransactionId);
                if(newTransaction != null && newTransaction.PayLater == null)
                {
                    var newTransactionRow = new TransactionRowViewModel(_transactionRepository, _dialogService, newTransaction);
                    SubscribeToTransactionRowEvents(newTransactionRow);
                    Transactions.Insert(0, newTransactionRow);

                }

            }
        }


        private void AddTransaction(ITransaction newTransaction)
        {
            var newTransactionRow = new TransactionRowViewModel(_transactionRepository, _dialogService, newTransaction);
            SubscribeToTransactionRowEvents(newTransactionRow);
            Transactions.Add(newTransactionRow);
        }


        private void RemoveTransactionRow(ITransactionRowViewModel transactionRow)
        {
            UnsubscribeToTransactionRowEvents(transactionRow);
            var matchedTransaction = Transactions.FirstOrDefault(t => t.Id == transactionRow.Id);
            if (matchedTransaction != null)
                Transactions.Remove(matchedTransaction);
        }

        private void HandleTransactionDeletion(object? sender, EventArgs<ITransactionRowViewModel> e)
        {
            RemoveTransactionRow(e.Item);
        }

        private void SubscribeToTransactionRowEvents(ITransactionRowViewModel transactionRow)
        {
            transactionRow.TransactionDeleted += HandleTransactionDeletion;
        }

        private void UnsubscribeToTransactionRowEvents(ITransactionRowViewModel transactionRow)
        {
            transactionRow.TransactionDeleted -= HandleTransactionDeletion;
        }


    }
}
