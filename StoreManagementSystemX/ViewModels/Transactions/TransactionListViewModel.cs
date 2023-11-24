using CommunityToolkit.Mvvm.Input;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
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
    class TransactionListViewModel : BaseViewModel, ITransactionListViewModel
    {
        public TransactionListViewModel(AuthContext authContext, IUnitOfWorkFactory unitOfWorkFactory, IDialogService dialogService, ITransactionCreationService transactionCreationService)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _authContext = authContext;
            _dialogService = dialogService;
            _transactionCreationService = transactionCreationService;
            Transactions = new ObservableCollection<ITransactionRowViewModel>();

            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                foreach (var t in unitOfWork.TransactionRepository.GetAll().OrderByDescending(t => t.DateTime))
                {
                    AddTransaction(t);
                }
            }


            if (Transactions.Count() > 0)
            {
                SelectedTransaction = Transactions.First();
            }
            NewTransactionCommand = new RelayCommand(NewTransactionCommandHandler);
        }

        private IUnitOfWorkFactory _unitOfWorkFactory { get; }
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
                using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
                {
                    var newTransaction = unitOfWork.TransactionRepository.GetById((Guid)newTransactionId);
                    var newTransactionRow = new TransactionRowViewModel(_unitOfWorkFactory, _dialogService, newTransaction);
                    SubscribeToTransactionRowEvents(newTransactionRow);
                    Transactions.Insert(0, newTransactionRow);
                }

            }
        }


        private void AddTransaction(Transaction newTransaction)
        {
            var newTransactionRow = new TransactionRowViewModel(_unitOfWorkFactory, _dialogService, newTransaction);
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
