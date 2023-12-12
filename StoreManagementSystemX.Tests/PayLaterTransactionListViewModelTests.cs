using NSubstitute;
using StoreManagementSystemX.Database.DAL;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using StoreManagementSystemX.Services;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.ViewModels.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Tests
{
    public class PayLaterTransactionListViewModelTests
    {
        //[Fact]
        //public void Only_pay_later_transactions_are_loaded()
        //{
        //    var transactions = new List<Transaction>()
        //    {
        //        new Transaction() { PayLater = new PayLater() },
        //        new Transaction() { PayLater = new PayLater() },
        //        new Transaction(),
        //        new Transaction() { PayLater = new PayLater() },
        //        new Transaction() { PayLater = new PayLater() },
        //        new Transaction(),
        //    };

        //    var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
        //    var unitOfWork = Substitute.For<IUnitOfWork>();
        //    unitOfWork.TransactionRepository.Find(default!).ReturnsForAnyArgs(transactions);
        //    unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);
        //    var dialogService = Substitute.For<IDialogService>();
        //    var authContext = new AuthContext(new User());
        //    var transactionCreationService = Substitute.For<ITransactionCreationService>();

        //    var sut = new PayLaterTransactionsViewModel(
        //        authContext,
        //        unitOfWorkFactory,
        //        dialogService,
        //        transactionCreationService
        //    );
            
        //    // assert
        //    Assert.Equal(transactions.Count(e => e.PayLater != null), sut.Transactions.Count);
        //}
    }
}
