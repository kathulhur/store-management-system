//using NSubstitute;
//using StoreManagementSystemX.Database.DAL.Interfaces;
//using StoreManagementSystemX.Database.Models;
//using StoreManagementSystemX.Services;
//using StoreManagementSystemX.Services.Interfaces;
//using StoreManagementSystemX.ViewModels.StockPurchases;
//using StoreManagementSystemX.ViewModels.Transactions;
//using StoreManagementSystemX.ViewModels.Transactions.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace StoreManagementSystemX.Tests
//{
//    public class TransactionListViewModelTests
//    {
//        [Fact]
//        public void All_records_loaded_on_initial_load()
//        {
//            var transactions = new List<Transaction>()
//            {
//                new Transaction{ DateTime = DateTime.Now},
//                new Transaction{ DateTime = DateTime.Now},
//                new Transaction{ DateTime = DateTime.Now},
//                new Transaction{ DateTime = DateTime.Now},
//                new Transaction{ DateTime = DateTime.Now.AddDays(-1) },
//            };

//            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
//            var unitOfWork = Substitute.For<IUnitOfWork>();
//            unitOfWork.TransactionRepository.Get().Returns(transactions);
//            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);

//            var dialogService = Substitute.For<IDialogService>();

//            var authContext = new AuthContext(new User());
//            var transactionCreationService = Substitute.For<ITransactionCreationService>();


//            var viewModel = new TransactionListViewModel(
//                authContext,
//                unitOfWorkFactory,
//                dialogService,
//                transactionCreationService
//            );

//            // assert
            
//            Assert.True(viewModel.Transactions.Any());
//            Assert.Equal(transactions.Count, viewModel.Transactions.Count);
//        }

//        [Fact]
//        public void Transactions_are_in_descending_order_on_initial_load()
//        {
//            var transactions = new List<Transaction>()
//            {
//                new Transaction{ DateTime = DateTime.Now.AddMinutes(1) },
//                new Transaction{ DateTime = DateTime.Now.AddMinutes(3) },
//                new Transaction{ DateTime = DateTime.Now.AddMinutes(4) },
//                new Transaction{ DateTime = DateTime.Now.AddMinutes(2) },
//                new Transaction{ DateTime = DateTime.Now.AddMinutes(-5) },
//            };

//            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
//            var unitOfWork = Substitute.For<IUnitOfWork>();
//            unitOfWork.TransactionRepository.Get().Returns(transactions);
//            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);

//            var dialogService = Substitute.For<IDialogService>();

//            var authContext = new AuthContext(new User());
//            var transactionCreationService = Substitute.For<ITransactionCreationService>();


//            var viewModel = new TransactionListViewModel(
//                authContext,
//                unitOfWorkFactory,
//                dialogService,
//                transactionCreationService
//            );

//            // assert

//            Assert.True(AreInDescendingOrder(viewModel.Transactions));
//        }

//        [Fact]
//        public void Transaction_removed_on_delete()
//        {

//            // arrange
//            var transaction = new Transaction { Id = Guid.NewGuid() };
//            var transactions = new List<Transaction>()
//            {
//                transaction,
//                new Transaction{ DateTime = DateTime.Now.AddMinutes(1) },
//                new Transaction{ DateTime = DateTime.Now.AddMinutes(3) },
//                new Transaction{ DateTime = DateTime.Now.AddMinutes(4) },
//                new Transaction{ DateTime = DateTime.Now.AddMinutes(2) },
//                new Transaction{ DateTime = DateTime.Now.AddMinutes(-5) },
//            };

//            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
//            var unitOfWork = Substitute.For<IUnitOfWork>();
//            unitOfWork.TransactionRepository.Get().Returns(transactions);
//            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);

//            var dialogService = Substitute.For<IDialogService>();
//            dialogService.ShowConfirmationDialog("title", "message").ReturnsForAnyArgs(true);

//            var authContext = new AuthContext(new User());
//            var transactionCreationService = Substitute.For<ITransactionCreationService>();


//            var viewModel = new TransactionListViewModel(
//                authContext,
//                unitOfWorkFactory,
//                dialogService,
//                transactionCreationService
//            );

//            // act
//            var transactionRowToDelete = viewModel.Transactions.FirstOrDefault(t => t.Id == transaction.Id);
//            transactionRowToDelete.DeleteCommand.Execute(null);

//            // assert
//            Assert.Equal(transactions.Count - 1, viewModel.Transactions.Count);
//        }

//        [Fact]
//        public void Selected_transaction_exist_when_there_are_records()
//        {

//            // arrange
//            var transactions = new List<Transaction>()
//            {
//                new Transaction{ DateTime = DateTime.Now.AddMinutes(1) },
//                new Transaction{ DateTime = DateTime.Now.AddMinutes(3) },
//                new Transaction{ DateTime = DateTime.Now.AddMinutes(4) },
//                new Transaction{ DateTime = DateTime.Now.AddMinutes(2) },
//                new Transaction{ DateTime = DateTime.Now.AddMinutes(-5) },
//            };

//            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
//            var unitOfWork = Substitute.For<IUnitOfWork>();
//            unitOfWork.TransactionRepository.Get().Returns(transactions);
//            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);

//            var dialogService = Substitute.For<IDialogService>();

//            var authContext = new AuthContext(new User());
//            var transactionCreationService = Substitute.For<ITransactionCreationService>();

//            // act
//            var viewModel = new TransactionListViewModel(
//                authContext,
//                unitOfWorkFactory,
//                dialogService,
//                transactionCreationService
//            );


//            // assert
//            Assert.NotNull(viewModel.SelectedTransaction);
//        }

//        [Fact]
//        public void Selected_transaction_holds_nothing_if_no_records_exists()
//        {

//            // arrange
//            var transactions = new List<Transaction>();

//            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
//            var unitOfWork = Substitute.For<IUnitOfWork>();
//            unitOfWork.TransactionRepository.Get().Returns(transactions);
//            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);

//            var dialogService = Substitute.For<IDialogService>();

//            var authContext = new AuthContext(new User());
//            var transactionCreationService = Substitute.For<ITransactionCreationService>();

//            // act
//            var viewModel = new TransactionListViewModel(
//                authContext,
//                unitOfWorkFactory,
//                dialogService,
//                transactionCreationService
//            );


//            // assert
//            Assert.Null(viewModel.SelectedTransaction);
//        }

//        [Fact]
//        public void New_transaction_gets_added_after_its_creation()
//        {
//            var newTransaction = new Transaction { Id = Guid.NewGuid() };
//            // arrange
//            var transactions = new List<Transaction>()
//            {
//                new Transaction { Id = Guid.NewGuid() },
//                new Transaction { Id = Guid.NewGuid() },
//                new Transaction { Id = Guid.NewGuid() },
//                new Transaction { Id = Guid.NewGuid() },
//            };

//            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
//            var unitOfWork = Substitute.For<IUnitOfWork>();
//            unitOfWork.TransactionRepository.GetById(newTransaction.Id).Returns(newTransaction);
//            unitOfWork.TransactionRepository.Get().Returns(transactions);
//            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);

//            var dialogService = Substitute.For<IDialogService>();

//            var authContext = new AuthContext(new User());
//            var transactionCreationService = Substitute.For<ITransactionCreationService>();
//            transactionCreationService.CreateNewTransaction(authContext).Returns(newTransaction.Id);

//            var viewModel = new TransactionListViewModel(
//                authContext,
//                unitOfWorkFactory,
//                dialogService,
//                transactionCreationService
//            );

//            // act
//            viewModel.NewTransactionCommand.Execute(null);

//            // assert
//            Assert.Equal(transactions.Count + 1, viewModel.Transactions.Count);
//        }

//        private bool AreInDescendingOrder(ObservableCollection<ITransactionRowViewModel> transactions)
//        {
//            if (transactions == null)
//                throw new ArgumentNullException();

//            if (transactions.Count() == 0)
//                return true;

//            var previousElement = transactions.First();
//            foreach (var currentElement in transactions)
//            {
//                if (previousElement.DateTime < currentElement.DateTime)
//                    return false;

//                previousElement = currentElement;
//            }

//            return true;
//        }

//    }
//}
