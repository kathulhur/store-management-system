using StoreManagementSystemX.Domain.Factories.Transactions;
using StoreManagementSystemX.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Tests
{
    public class TransactionRepositoryTests
    {

        private TransactionRepository CreateTransactionRepositoryWithSingleTransaction()
        {
            TransactionRepository transactionRepository = new TransactionRepository();
            TransactionFactory transactionFactory = new TransactionFactory();

            var transaction = transactionFactory.Create(Guid.NewGuid());

            Assert.Empty(transactionRepository.GetAll());
            transactionRepository.Add(transaction);

            var transactions = transactionRepository.GetAll();
            Assert.True(transactions.Any());
            Assert.True(transactions.Count() == 1);


            return transactionRepository;
        }


        [Fact]
        public void Transaction_gets_deleted_on_delete()
        {
            // Arrange
            var transactionRepository = CreateTransactionRepositoryWithSingleTransaction();
            var transaction = transactionRepository.GetAll().First();

            // Act
            transactionRepository.Remove(transaction.Id);

            //Assert
            Assert.Empty(transactionRepository.GetAll());
        }

    }
}
