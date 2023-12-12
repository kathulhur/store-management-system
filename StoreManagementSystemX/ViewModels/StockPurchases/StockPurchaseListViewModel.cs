using CommunityToolkit.Mvvm.Input;
using StoreManagementSystemX.Database.DAL;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
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
        public StockPurchaseListViewModel(AuthContext authContext, IUnitOfWorkFactory unitOfWorkFactory, IDialogService dialogService, IStockPurchaseCreationService stockPurchaseCreationService)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _authContext = authContext;
            _createStockPurchaseWindowService = stockPurchaseCreationService;
            StockPurchases = new ObservableCollection<StockPurchaseViewModel>();

            using(var unitOfWork = unitOfWorkFactory.CreateUnitOfWork())
            {
                foreach(var stockPurchase in unitOfWork.StockPurchaseRepository.Get(null, sp => sp.OrderByDescending(e => e.DateTime), "StockPurchaseProducts, StockPurchaseProducts.Product"))
                {
                    StockPurchases.Add(new StockPurchaseViewModel(stockPurchase));
                }
            }
            if (StockPurchases.Any())
            {
                SelectedStockPurchase = StockPurchases.First();
            }

            NewStockPurchaseCommand = new RelayCommand(OnNewTransaction);
        }

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        private readonly AuthContext _authContext;

        private IStockPurchaseCreationService _createStockPurchaseWindowService;

        public ObservableCollection<StockPurchaseViewModel> StockPurchases { get; }

        public StockPurchaseViewModel? SelectedStockPurchase { get; set; }

        public void AddStockPurchase(StockPurchase stockPurchase)
        {
            StockPurchases.Insert(0, new StockPurchaseViewModel(stockPurchase));
        }

        public ICommand NewStockPurchaseCommand { get; }

        public void OnNewTransaction()
        {
            var newStockPurchaseId = _createStockPurchaseWindowService.CreateStockPurchase(_authContext);
            {
                using(var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
                {
                    if(newStockPurchaseId != null)
                    {
                        var newStockPurchase = unitOfWork.StockPurchaseRepository.GetById((Guid)newStockPurchaseId, "StockPurchaseProducts, StockPurchaseProducts.Product");
                        if(newStockPurchase != null)
                        {
                            AddStockPurchase(newStockPurchase);
                        }
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
