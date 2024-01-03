using Microsoft.EntityFrameworkCore;
using StoreManagementSystemX.Database;
using StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases.Interfaces;
using StoreManagementSystemX.Domain.Factories.StockPurchases.Interfaces;
using StoreManagementSystemX.Domain.Repositories.StockPurchases.Interfaces;
using StoreManagementSystemX.Infrastructure.DTO;
using StoreManagementSystemX.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Infrastructure.Persistence
{
    public class StockPurchaseRepositoryImpl : IStockPurchaseRepository
    {
        private readonly IStockPurchaseFactory _stockPurchaseFactory;
        private readonly DbContext _dbContext;
        private readonly DbSet<StockPurchaseDBModel> _stockPurchases;
        private readonly DbSet<StockPurchaseProductDBModel> _stockPurchaseProducts;

        public StockPurchaseRepositoryImpl(IStockPurchaseFactory stockPurchaseFactory)
        {
            _stockPurchaseFactory = stockPurchaseFactory;
            _dbContext = new StoreContext();
            _stockPurchases = _dbContext.Set<StockPurchaseDBModel>();
            _stockPurchaseProducts = _dbContext.Set<StockPurchaseProductDBModel>();
        }

        public void Add(IStockPurchase newEntity)
        {
            var stockPurchaseToAdd = new StockPurchaseDBModel
            {
                StockManagerId = newEntity.StockManagerId,
                Id = newEntity.Id,
                DateTime = newEntity.DateTime,
            };

            _stockPurchases.Add(stockPurchaseToAdd);

            foreach(var transactionProduct in newEntity.StockPurchaseProducts)
            {
                var transactionProductToAdd = new StockPurchaseProductDBModel
                {
                    StockPurchaseId = stockPurchaseToAdd.Id,
                    ProductId = transactionProduct.ProductId,
                    Barcode = transactionProduct.Barcode,
                    Name = transactionProduct.Name,
                    Price = transactionProduct.Price,
                    QuantityBought = transactionProduct.QuantityBought,
                };
                _stockPurchaseProducts.Add(transactionProductToAdd);
            }

            _dbContext.SaveChanges();
        }

        private StockPurchaseDTO ToStockPurchaseDTO(StockPurchaseDBModel stockPurchase)
        {
            var stockPurchaseProductDTOs = new List<StockPurchaseProductDTO>();
            foreach(var stockPurchaseProduct in  stockPurchase.StockPurchaseProducts)
            {
                stockPurchaseProductDTOs.Add(new StockPurchaseProductDTO(stockPurchaseProduct));
            }

            return new StockPurchaseDTO(stockPurchase, stockPurchaseProductDTOs);
            
        }

        public IEnumerable<IStockPurchase> GetAll()
        {
            var allStockPurchases = new List<IStockPurchase>();
            foreach(var stockPurchase in _stockPurchases.Include(e => e.StockPurchaseProducts))
            {
                allStockPurchases.Add(_stockPurchaseFactory.Reconstitute(ToStockPurchaseDTO(stockPurchase)));
            }

            return allStockPurchases;

        }

        public IStockPurchase? GetById(Guid id)
        {
            var storedStockPurchase = _stockPurchases.Include(e => e.StockPurchaseProducts).SingleOrDefault(e => e.Id == id);
            if(storedStockPurchase != null)
            {
                var stockPurchaseDTO = ToStockPurchaseDTO(storedStockPurchase);
                return _stockPurchaseFactory.Reconstitute(stockPurchaseDTO);
            }

            return null;
        }

        public void Remove(Guid id)
        {
            var stockPurchaseToRemove = _stockPurchases.Find(id);
            if (stockPurchaseToRemove != null)
            {
                _stockPurchases.Remove(stockPurchaseToRemove);
            }
            _dbContext.SaveChanges();
        }

        public void Update(IStockPurchase updatedStockPurchase)
        {
            var stockPurchaseToRemove = _stockPurchases.Find(updatedStockPurchase.Id);
            if (stockPurchaseToRemove != null)
            {
                _stockPurchases.Update(stockPurchaseToRemove);
            }

            _dbContext.SaveChanges();
        }
    }
}
