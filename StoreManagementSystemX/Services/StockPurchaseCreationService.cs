using StoreManagementSystemX.Database.DAL.Interfaces;
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
        public StockPurchaseCreationService(IUnitOfWorkFactory unitOfWorkFactory, IDialogService dialogService)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _dialogService = dialogService;
        }

        private readonly IDialogService _dialogService;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public Guid? CreateStockPurchase(AuthContext authContext)
        {
            Guid? newStockPurchase = null;
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var window = new CreateStockPurchaseWindow(authContext, unitOfWork, _dialogService, (Guid stockPurchaseId) =>
                {
                    newStockPurchase = stockPurchaseId;
                });
                window.ShowDialog();

            }
            return newStockPurchase;
        }
    }
}
