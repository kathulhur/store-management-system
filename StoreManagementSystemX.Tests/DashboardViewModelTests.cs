
using NSubstitute;
using NSubstitute.Extensions;
using StoreManagementSystemX.Database.DAL;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using StoreManagementSystemX.Services;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.ViewModels;
using System.Reflection;

namespace StoreManagementSystemX.Tests
{
    public class DashboardViewModelTests
    {

        [Fact]
        public void All_transactions_added_on_initialization()
        {

            // arrange
            var transactions = new List<Transaction>()
            {
                new Transaction{Id = Guid.NewGuid(), DateTime = DateTime.Now},
                new Transaction{Id = Guid.NewGuid(), DateTime = DateTime.Now},
                new Transaction{Id = Guid.NewGuid(), DateTime = DateTime.Now},
                new Transaction{Id = Guid.NewGuid(), DateTime = DateTime.Now},
                new Transaction{Id = Guid.NewGuid(), DateTime = DateTime.Now}
            };

            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            unitOfWork.TransactionRepository.GetAll().Returns(transactions);

            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);

            var dialogService = Substitute.For<IDialogService>();
            var authenticatedUser = new User { Id = Guid.NewGuid() };
            var authContext = new AuthContext(authenticatedUser);
            var transactionCreationService = Substitute.For<ITransactionCreationService>();

            // act: Initialize
            var dashboardViewModel = new DashboardViewModel(authContext, unitOfWorkFactory, dialogService, transactionCreationService);


            // assert
            Assert.Equal(5, dashboardViewModel.TransactionsToday.Count);
            Assert.Equal(5, dashboardViewModel.TotalTransactionsToday);
        }


        [Fact]
        public void Daily_revenue_correctly_computed()
        {
            // arrange

            var transactions = new List<Transaction>()
            {
                // Sales = 100 + 16 = 116
                // Profit = Sales - (50 + 10) = 56
                new Transaction{Id = Guid.NewGuid(), DateTime = DateTime.Now, TransactionProducts = {
                    new TransactionProduct { ProductId = Guid.NewGuid(), QuantityBought = 5, CostPrice = 10, PriceSold = 20 },
                    new TransactionProduct { ProductId = Guid.NewGuid(), QuantityBought = 2, CostPrice = 5, PriceSold = 8 }
                }},
                // Sales = 80 + 8 = 88
                // Profit = Sales - (40 + 5) = 43
                new Transaction{Id = Guid.NewGuid(), DateTime = DateTime.Now, TransactionProducts = {
                    new TransactionProduct { ProductId = Guid.NewGuid(), QuantityBought = 4, CostPrice = 10, PriceSold = 20 },
                    new TransactionProduct { ProductId = Guid.NewGuid(), QuantityBought = 1, CostPrice = 5, PriceSold = 8 }
                }},
            };

            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            unitOfWork.TransactionRepository.GetAll().Returns(transactions);

            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);

            var dialogService = Substitute.For<IDialogService>();
            var authenticatedUser = new User { Id = Guid.NewGuid() };
            var authContext = new AuthContext(authenticatedUser);

            var transactionCreationService = Substitute.For<ITransactionCreationService>();

            // act: Initialize
            var dashboardViewModel = new DashboardViewModel(authContext, unitOfWorkFactory, dialogService, transactionCreationService);

            // assert
            Assert.Equal(204, dashboardViewModel.TotalRevenueToday);

        }


        [Fact]
        public void Profit_revenue_correctly_computed()
        {
            // arrange
            var transactions = new List<Transaction>()
            {
                // Sales = 100 + 16 = 116
                // Profit = Sales - (50 + 10) = 56
                new Transaction{Id = Guid.NewGuid(), DateTime = DateTime.Now, TransactionProducts = {
                    new TransactionProduct { ProductId = Guid.NewGuid(), QuantityBought = 5, CostPrice = 10, PriceSold = 20 },
                    new TransactionProduct { ProductId = Guid.NewGuid(), QuantityBought = 2, CostPrice = 5, PriceSold = 8 }
                }},
                // Sales = 80 + 8 = 88
                // Profit = Sales - (40 + 5) = 43
                new Transaction{Id = Guid.NewGuid(), DateTime = DateTime.Now, TransactionProducts = {
                    new TransactionProduct { ProductId = Guid.NewGuid(), QuantityBought = 4, CostPrice = 10, PriceSold = 20 },
                    new TransactionProduct { ProductId = Guid.NewGuid(), QuantityBought = 1, CostPrice = 5, PriceSold = 8 }
                }},
            };

            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.TransactionRepository.GetAll().Returns(transactions);
            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);



            var dialogService = Substitute.For<IDialogService>();
            var authenticatedUser = new User { Id = Guid.NewGuid() };
            var authContext = new AuthContext(authenticatedUser);
            var transactionCreationService = Substitute.For<ITransactionCreationService>();

            // act: Initialize
            var dashboardViewModel = new DashboardViewModel(authContext, unitOfWorkFactory, dialogService, transactionCreationService);

            // assert
            Assert.Equal(99, dashboardViewModel.TotalProfitToday);
            
        }


        [Fact]
        public void New_transaction_added_to_transaction_list_after_its_creation()
        {
            // arrange
            var newTransaction = new Transaction();

            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();

            var unitOfWork = Substitute.For<IUnitOfWork>();

            unitOfWork.TransactionRepository.GetById(Guid.NewGuid()).ReturnsForAnyArgs(newTransaction);
            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);

            var dialogService = Substitute.For<IDialogService>();
            var authenticatedUser = new User { Id = Guid.NewGuid() };
            var authContext = new AuthContext(authenticatedUser);

            var transactionCreationService = Substitute.For<ITransactionCreationService>();

            transactionCreationService.CreateNewTransaction(authContext).Returns(Guid.NewGuid());

            var dashboardViewModel = new DashboardViewModel(authContext, unitOfWorkFactory, dialogService, transactionCreationService);

            // act: Initialize
            dashboardViewModel.NewTransactionCommand.Execute(null);

            Assert.True(dashboardViewModel.TransactionsToday.Any());
        }


        [Fact]
        public void Selected_transaction_not_null_when_transaction_list_is_not_empty()
        {
            // arrange

            var transactions = new List<Transaction>() { new Transaction { DateTime = DateTime.Now } };

            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
            unitOfWorkFactory.CreateUnitOfWork().ReturnsForAll(Substitute.For<IUnitOfWork>());

            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.TransactionRepository.GetAll().Returns(transactions);

            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);

            var dialogService = Substitute.For<IDialogService>();

            var authenticatedUser = new User { Id = Guid.NewGuid() };

            var authContext = new AuthContext(authenticatedUser);
            var transactionCreationService = Substitute.For<ITransactionCreationService>();

            // act: Initialize
            var dashboardViewModel = new DashboardViewModel(authContext, unitOfWorkFactory, dialogService, transactionCreationService);

            Assert.NotNull(dashboardViewModel.SelectedTransaction);
        }

        [Fact]
        public void Selected_transaction_null_when_transaction_list_empty()
        {
            // arrange
            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
            unitOfWorkFactory.CreateUnitOfWork().ReturnsForAll(Substitute.For<IUnitOfWork>());


            unitOfWorkFactory.CreateUnitOfWork().TransactionRepository.Find(default!).ReturnsForAnyArgs(new List<Transaction>());

            var dialogService = Substitute.For<IDialogService>();
            var authenticatedUser = new User { Id = Guid.NewGuid() };
            var authContext = new AuthContext(authenticatedUser);
            var transactionCreationService = Substitute.For<ITransactionCreationService>();

            // act: Initialize
            var dashboardViewModel = new DashboardViewModel(authContext, unitOfWorkFactory, dialogService, transactionCreationService);

            Assert.Null(dashboardViewModel.SelectedTransaction);
        }


    }
}