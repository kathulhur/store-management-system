using CommunityToolkit.Mvvm.Input;
using StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases.Interfaces;
using StoreManagementSystemX.Domain.Repositories.StockPurchases.Interfaces;
using StoreManagementSystemX.Services;
using StoreManagementSystemX.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.StockPurchases
{
    public class StockPurchaseListViewModel : BaseViewModel
    {
        public StockPurchaseListViewModel(AuthContext authContext, Domain.Repositories.StockPurchases.Interfaces.IStockPurchaseRepository stockPurchaseRepository, IDialogService dialogService, IStockPurchaseCreationService stockPurchaseCreationService)
        {
            _stockPurchaseRepository = stockPurchaseRepository;
            _authContext = authContext;
            _createStockPurchaseWindowService = stockPurchaseCreationService;
            StockPurchases = new ObservableCollection<StockPurchaseViewModel>();

            foreach (var stockPurchase in stockPurchaseRepository.GetAll())
            {
                StockPurchases.Add(new StockPurchaseViewModel(stockPurchase));
            }

            if (StockPurchases.Any())
            {
                SelectedStockPurchase = StockPurchases.First();
            }

            NewStockPurchaseCommand = new RelayCommand(OnNewTransaction);
        }

        private readonly Domain.Repositories.StockPurchases.Interfaces.IStockPurchaseRepository _stockPurchaseRepository;

        private readonly AuthContext _authContext;

        private IStockPurchaseCreationService _createStockPurchaseWindowService;

        public ObservableCollection<StockPurchaseViewModel> StockPurchases { get; }

        public StockPurchaseViewModel? SelectedStockPurchase { get; set; }

        public void AddStockPurchase(IStockPurchase stockPurchase)
        {
            StockPurchases.Insert(0, new StockPurchaseViewModel(stockPurchase));
        }

        public ICommand NewStockPurchaseCommand { get; }

        public void OnNewTransaction()
        {
            var newStockPurchaseId = _createStockPurchaseWindowService.CreateStockPurchase(_authContext);
            {
                if(newStockPurchaseId != null)
                {
                    var newStockPurchase = _stockPurchaseRepository.GetById((Guid)newStockPurchaseId);
                    if(newStockPurchase != null)
                    {
                        AddStockPurchase(newStockPurchase);
                    }
                }
            }
        }

        public decimal TotalCost
        {
            get
            {
                if (SelectedStockPurchase != null)
                    return SelectedStockPurchase.StockPurchaseProducts.Sum(e => e.TotalPrice);

                return 0;
            }
        }



    }
}
