using NSubstitute;
using NSubstitute.ReturnsExtensions;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using StoreManagementSystemX.Services;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.ViewModels.StockPurchases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace StoreManagementSystemX.Tests
{
    public class StockPurchaseViewModelTests
    {

        private AuthContext GetAuthContext()
            => new AuthContext(new User());

        [Fact]
        public void Records_are_loaded()
        {
            // arrange
            var stockPurchases = new List<StockPurchase>()
            {
                new StockPurchase{ DateTime = DateTime.Now.AddMinutes(1) },
                new StockPurchase{ DateTime = DateTime.Now.AddMinutes(2) },
                new StockPurchase{ DateTime = DateTime.Now.AddMinutes(4) },
                new StockPurchase{ DateTime = DateTime.Now },
            };
            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.StockPurchaseRepository.Get().Returns(stockPurchases);
            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);
            var authContext = GetAuthContext();
            var dialogService = Substitute.For<IDialogService>();
            var stockPurchaseCreationService = Substitute.For<IStockPurchaseCreationService>();

            //act
            var stockPurchaseViewModel = new StockPurchaseListViewModel(
                authContext,
                unitOfWorkFactory,
                dialogService,
                stockPurchaseCreationService
            );

            // assert
            Assert.Equal(stockPurchases.Count, stockPurchaseViewModel.StockPurchases.Count);
        }

        [Fact]
        public void Records_are_in_descending_order_on_initial_load()
        {
            // arrange
            var stockPurchases = new List<StockPurchase>()
            {
                new StockPurchase{ DateTime = DateTime.Now.AddMinutes(1) },
                new StockPurchase{ DateTime = DateTime.Now.AddMinutes(2) },
                new StockPurchase{ DateTime = DateTime.Now.AddMinutes(4) },
                new StockPurchase{ DateTime = DateTime.Now },
            };
            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.StockPurchaseRepository.Get().Returns(stockPurchases);
            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);
            var authContext = GetAuthContext();
            var dialogService = Substitute.For<IDialogService>();
            var stockPurchaseCreationService = Substitute.For<IStockPurchaseCreationService>();

            //act
            var stockPurchaseListViewModel = new StockPurchaseListViewModel(
                authContext,
                unitOfWorkFactory,
                dialogService,
                stockPurchaseCreationService
            );

            // assert
            Assert.True(AreInDescendingOrder(stockPurchaseListViewModel.StockPurchases));
        }

        [Fact]
        public void Selected_stock_purchase_holds_the_first_record_if_records_exists_on_initial_load()
        {
            // arrange

            var firstElementId = Guid.NewGuid();
            var stockPurchases = new List<StockPurchase>()
            {
                new StockPurchase{ DateTime = DateTime.Now.AddMinutes(1) },
                new StockPurchase{ DateTime = DateTime.Now.AddMinutes(2) },
                new StockPurchase{ Id = firstElementId, DateTime = DateTime.Now.AddMinutes(4) },
                new StockPurchase{ DateTime = DateTime.Now },
            };
            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.StockPurchaseRepository.Get().Returns(stockPurchases);
            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);
            var authContext = GetAuthContext();
            var dialogService = Substitute.For<IDialogService>();
            var stockPurchaseCreationService = Substitute.For<IStockPurchaseCreationService>();

            //act
            var stockPurchaseListViewModel = new StockPurchaseListViewModel(
                authContext,
                unitOfWorkFactory,
                dialogService,
                stockPurchaseCreationService
            );

            // assert
            Assert.NotNull(stockPurchaseListViewModel.SelectedStockPurchase);
            Assert.Equal(firstElementId, stockPurchaseListViewModel.SelectedStockPurchase?.Id);
        }

        [Fact]
        public void Selected_stock_purchase_holds_nothing_if_there_are_no_records_on_initial_load()
        {
            // arrange

            var stockPurchases = new List<StockPurchase>();
            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.StockPurchaseRepository.Get().Returns(stockPurchases);
            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);
            var authContext = GetAuthContext();
            var dialogService = Substitute.For<IDialogService>();
            var stockPurchaseCreationService = Substitute.For<IStockPurchaseCreationService>();

            //act
            var stockPurchaseListViewModel = new StockPurchaseListViewModel(
                authContext,
                unitOfWorkFactory,
                dialogService,
                stockPurchaseCreationService
            );

            // assert
            Assert.Null(stockPurchaseListViewModel.SelectedStockPurchase);
        }

        [Fact]
        public void Total_cost_is_consistent_with_the_selected_stock_purchase()
        {
            // arrange
            var firstElementId = Guid.NewGuid();
            var stockPurchases = new List<StockPurchase>()
            {
                new StockPurchase{ DateTime = DateTime.Now, StockPurchaseProducts =
                    {// total cost = 50
                        new StockPurchaseProduct { Price = 10, QuantityBought = 1 },
                        new StockPurchaseProduct { Price = 20, QuantityBought = 2 },
                    } 
                },
            };
            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.StockPurchaseRepository.Get().Returns(stockPurchases);
            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);

            var authContext = GetAuthContext();
            var dialogService = Substitute.For<IDialogService>();
            var stockPurchaseCreationService = Substitute.For<IStockPurchaseCreationService>();

            //act
            var stockPurchaseListViewModel = new StockPurchaseListViewModel(
                authContext,
                unitOfWorkFactory,
                dialogService,
                stockPurchaseCreationService
            );

            var selectedStockPurchaseTotalCost = stockPurchaseListViewModel.SelectedStockPurchase.StockPurchaseProducts.Sum(e => e.TotalPrice);
            // assert
            Assert.Equal(selectedStockPurchaseTotalCost, stockPurchaseListViewModel.TotalCost);
        }



        [Fact]
        public void Total_cost_updates_when_selected_stock_purchase_changes()
        {
            // arrange
            var firstElementId = Guid.NewGuid();
            var stockPurchases = new List<StockPurchase>()
            {
                new StockPurchase{ DateTime = DateTime.Now.AddMinutes(1), StockPurchaseProducts =
                    {// total cost = 50
                        new StockPurchaseProduct { Price = 10, QuantityBought = 1 },
                        new StockPurchaseProduct { Price = 20, QuantityBought = 2 },
                    }
                },
                new StockPurchase{ DateTime = DateTime.Now, StockPurchaseProducts =
                    {// total cost = 100
                        new StockPurchaseProduct { Price = 10, QuantityBought = 1 },
                        new StockPurchaseProduct { Price = 20, QuantityBought = 2 },
                        new StockPurchaseProduct { Price = 25, QuantityBought = 2 },
                    }
                },
            };
            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.StockPurchaseRepository.Get().Returns(stockPurchases);
            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);

            var authContext = GetAuthContext();
            var dialogService = Substitute.For<IDialogService>();
            var stockPurchaseCreationService = Substitute.For<IStockPurchaseCreationService>();

            var stockPurchaseListViewModel = new StockPurchaseListViewModel(
                authContext,
                unitOfWorkFactory,
                dialogService,
                stockPurchaseCreationService
            );

            var selectedStockPurchaseTotalCost = stockPurchaseListViewModel.SelectedStockPurchase.StockPurchaseProducts.Sum(e => e.TotalPrice);
            Assert.Equal(selectedStockPurchaseTotalCost, stockPurchaseListViewModel.TotalCost);

            //act
            stockPurchaseListViewModel.SelectedStockPurchase = stockPurchaseListViewModel.StockPurchases.Last();
            selectedStockPurchaseTotalCost = stockPurchaseListViewModel.SelectedStockPurchase.StockPurchaseProducts.Sum(e => e.TotalPrice);
            Assert.Equal(selectedStockPurchaseTotalCost, stockPurchaseListViewModel.TotalCost);


            //assert
        }

        [Fact]
        public void New_stock_purchase_gets_added_in_list_creation()
        {
            // arrange
            var newStockPurchase = new StockPurchase
            {
                Id = Guid.NewGuid(),
                DateTime = DateTime.Now.AddMinutes(1),
                StockPurchaseProducts =
                    {// total cost = 50
                        new StockPurchaseProduct { Price = 10, QuantityBought = 1 },
                        new StockPurchaseProduct { Price = 20, QuantityBought = 2 },
                    }
            };

            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.StockPurchaseRepository.Get().Returns(new List<StockPurchase>());
            unitOfWork.StockPurchaseRepository.GetById(newStockPurchase.Id).Returns(new StockPurchase { Id = newStockPurchase.Id });
            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);

            var authContext = GetAuthContext();
            var dialogService = Substitute.For<IDialogService>();
            var stockPurchaseCreationService = Substitute.For<IStockPurchaseCreationService>();
            stockPurchaseCreationService.CreateStockPurchase(authContext).Returns(newStockPurchase.Id);

            var stockPurchaseListViewModel = new StockPurchaseListViewModel(
                authContext,
                unitOfWorkFactory,
                dialogService,
                stockPurchaseCreationService
            );
            Assert.False(stockPurchaseListViewModel.StockPurchases.Any());

            //act
            stockPurchaseListViewModel.NewStockPurchaseCommand.Execute(null);

            //assert
            Assert.True(stockPurchaseListViewModel.StockPurchases.Any());
        }

        [Fact]
        public void Records_count_remain_the_same_on_cancelled_stock_purchase_creation()
        {
            // arrange
            var newStockPurchase = new StockPurchase
            {
                Id = Guid.NewGuid(),
                DateTime = DateTime.Now.AddMinutes(1),
                StockPurchaseProducts =
                    {// total cost = 50
                        new StockPurchaseProduct { Price = 10, QuantityBought = 1 },
                        new StockPurchaseProduct { Price = 20, QuantityBought = 2 },
                    }
            };

            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.StockPurchaseRepository.Get().Returns(new List<StockPurchase>());
            unitOfWork.StockPurchaseRepository.GetById(newStockPurchase.Id).Returns(new StockPurchase { Id = newStockPurchase.Id });
            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);

            var authContext = GetAuthContext();
            var dialogService = Substitute.For<IDialogService>();
            var stockPurchaseCreationService = Substitute.For<IStockPurchaseCreationService>();
            stockPurchaseCreationService.CreateStockPurchase(authContext).ReturnsNull();

            var stockPurchaseListViewModel = new StockPurchaseListViewModel(
                authContext,
                unitOfWorkFactory,
                dialogService,
                stockPurchaseCreationService
            );
            Assert.False(stockPurchaseListViewModel.StockPurchases.Any());

            //act
            stockPurchaseListViewModel.NewStockPurchaseCommand.Execute(null);

            //assert
            Assert.False(stockPurchaseListViewModel.StockPurchases.Any());
        }



        private bool AreInDescendingOrder(ObservableCollection<StockPurchaseViewModel> stockPurchases)
        {
            if (stockPurchases == null)
                throw new ArgumentNullException();

            if (stockPurchases.Count() == 0)
                return true;

            var previousElement = stockPurchases.First();
            foreach(var currentElement in stockPurchases)
            {
                if (previousElement.DateTime < currentElement.DateTime)
                    return false;

                previousElement = currentElement;
            }

            return true;
        }
    }
}
