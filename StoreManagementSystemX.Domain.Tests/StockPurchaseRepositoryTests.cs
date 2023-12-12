using StoreManagementSystemX.Domain.Factories.StockPurchases;
using StoreManagementSystemX.Domain.Factories.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Repositories
{
    public class StockPurchaseRepositoryTests
    {
        private StockPurchaseRepository CreateStockpurchaseRepositoryWithSingleStockPurchase()
        {
            var stockPurhcaseRepository = new StockPurchaseRepository();
            var stockPurchaseFactory = new StockPurchaseFactory();

            var stockPurchase = stockPurchaseFactory.Create(Guid.NewGuid());

            Assert.Empty(stockPurhcaseRepository.GetAll());
            stockPurhcaseRepository.Add(stockPurchase);

            var stockPurchases = stockPurhcaseRepository.GetAll();
            Assert.True(stockPurchases.Any());
            Assert.True(stockPurchases.Count() == 1);

            return stockPurhcaseRepository;
        }


        [Fact]
        public void StockPurchase_gets_deleted_on_delete()
        {
            // Arrange
            var stockPurchaseRepository = CreateStockpurchaseRepositoryWithSingleStockPurchase();
            var stockPurchase = stockPurchaseRepository.GetAll().First();

            // Act
            stockPurchaseRepository.Remove(stockPurchase.Id);

            //Assert
            Assert.Empty(stockPurchaseRepository.GetAll());
        }
    }
}
