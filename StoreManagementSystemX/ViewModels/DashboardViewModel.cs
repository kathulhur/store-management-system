using CommunityToolkit.Mvvm.Input;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Services;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.ViewModels.Interfaces;
using StoreManagementSystemX.ViewModels.Transactions;
using StoreManagementSystemX.ViewModels.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels
{
    public class DashboardViewModel : BaseViewModel, IDashboardViewModel
    {
        

        public DashboardViewModel(AuthContext authContext, IUnitOfWorkFactory unitOfWorkFactory, IDialogService dialogService, ITransactionCreationService transactionCreationService) 
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _authContext = authContext;
            _dialogService = dialogService;
            _transactionCreationService = transactionCreationService;
            TransactionsToday = new ObservableCollection<ITransactionRowViewModel>();
            using(var unitOfWork = unitOfWorkFactory.CreateUnitOfWork())
            {
                var transactions = unitOfWork.TransactionRepository.Get(t => t.DateTime.Date == DateTime.Now.Date, t => t.OrderByDescending(e => e.DateTime), "TransactionProducts");
                foreach (var transaction in transactions)
                {
                    TransactionsToday.Add(new TransactionRowViewModel(unitOfWorkFactory, _dialogService, transaction));
                }
            }
            NewTransactionCommand = new RelayCommand(NewTransactionCommandHandler);
            if (TransactionsToday.Any())
            {
                SelectedTransaction = TransactionsToday.First();
                _totalPurchased = "Total: ₱ " + SelectedTransaction.TransactionProducts.Sum(tp => tp.TotalPrice);
            } else
            {
                _selectedTransaction = null;
                _totalPurchased = "Total: ₱ 0";
            }
        }

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly AuthContext _authContext;
        private readonly IDialogService _dialogService;
        private readonly ITransactionCreationService _transactionCreationService;

        public DateTime DateToday => DateTime.Now;

        public ObservableCollection<ITransactionRowViewModel> TransactionsToday {  get; } 

        public int TotalTransactionsToday
        {
            get => TransactionsToday.Count;
        }

        public decimal TotalRevenueToday
        {
            get => TransactionsToday.Sum(t => t.TotalPrice);
        }

        public decimal TotalProfitToday
        {
            get => TotalRevenueToday - TransactionsToday.Sum(t => t.TotalCost);
        }

        private ITransactionRowViewModel? _selectedTransaction;
        public ITransactionRowViewModel? SelectedTransaction 
        {
            get => _selectedTransaction;
            set
            {
                _selectedTransaction = value;
                if(_selectedTransaction != null)
                {
                    TotalPurchased = "Total: ₱ " + _selectedTransaction.TransactionProducts.Sum(tp => tp.TotalPrice);
                } else
                {
                    TotalPurchased = "Total: ₱ 0";
                }
                NotifyPropertiesChanged();
            }
        }

        private string _totalPurchased;

        public string TotalPurchased
        {
            get => _totalPurchased;
            private set
            {
                SetProperty(ref _totalPurchased, value);
            }
        }

        public ICommand NewTransactionCommand { get; }

        private void NotifyPropertiesChanged()
        {
            OnPropertyChanged(nameof(TotalTransactionsToday));
            OnPropertyChanged(nameof(TotalRevenueToday));
            OnPropertyChanged(nameof(TotalProfitToday));
        }

        private void NewTransactionCommandHandler()
        {
            var newTransactionId = _transactionCreationService.CreateNewTransaction(_authContext);
            if(newTransactionId != null)
            {
                using(var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
                {
                    var newTransaction = unitOfWork.TransactionRepository.GetById((Guid) newTransactionId, "TransactionProducts,TransactionProducts.Product");
                    if(newTransaction != null)
                    {
                        TransactionsToday.Insert(0, new TransactionRowViewModel(_unitOfWorkFactory, _dialogService, newTransaction));
                    }
                    NotifyPropertiesChanged();
                }
            }

        }






    }
}
