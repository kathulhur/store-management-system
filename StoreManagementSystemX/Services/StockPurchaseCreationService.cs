using StoreManagementSystemX.Domain.Repositories.Products.Interfaces;
using StoreManagementSystemX.Domain.Repositories.StockPurchases.Interfaces;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.Views.StockPurchases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Services
{
    class StockPurchaseCreationService : IStockPurchaseCreationService
    {
        public StockPurchaseCreationService(Domain.Repositories.StockPurchases.Interfaces.IStockPurchaseRepository stockPurchaseRepository, Domain.Repositories.Products.Interfaces.IProductRepository productRepository, IDialogService dialogService)
        {
            _stockPurchaseRepository = stockPurchaseRepository;
            _productRepository = productRepository;
            _dialogService = dialogService;
        }

        private readonly IDialogService _dialogService;
        private readonly Domain.Repositories.StockPurchases.Interfaces.IStockPurchaseRepository _stockPurchaseRepository;
        private readonly Domain.Repositories.Products.Interfaces.IProductRepository _productRepository;

        public Guid? CreateStockPurchase(AuthContext authContext)
        {
            Guid? newStockPurchase = null;
            var window = new CreateStockPurchaseWindow(authContext, _stockPurchaseRepository, _productRepository, _dialogService, (Guid stockPurchaseId) =>
            {
                newStockPurchase = stockPurchaseId;
            });
            window.ShowDialog();

            return newStockPurchase;
        }
    }
}
