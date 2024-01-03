using Microsoft.EntityFrameworkCore;
using StoreManagementSystemX.Database;
using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces;
using StoreManagementSystemX.Domain.Factories.Transactions;
using StoreManagementSystemX.Domain.Factories.Transactions.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Transactions.Interfaces;
using StoreManagementSystemX.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Infrastructure.Persistence
{
    public class TransactionRepositoryImpl : ITransactionRepository
    {
        private readonly ITransactionFactory _transactionFactory;
        private readonly DbContext _dbContext;
        private readonly DbSet<TransactionDBModel> _transactions;
        private readonly DbSet<TransactionProductDBModel> _transactionProducts;

        public TransactionRepositoryImpl(TransactionFactory transactionFactory)
        {
            _transactionFactory = transactionFactory;
            _dbContext = new StoreContext();
            _transactions = _dbContext.Set<TransactionDBModel>();
            _transactionProducts = _dbContext.Set<TransactionProductDBModel>();
        }

        public void Add(ITransaction newTransaction)
        {
            var transactionDbModel = ToTransactionDBModel(newTransaction);
            _transactions.Add(transactionDbModel);
            _dbContext.SaveChanges();

        }

        private TransactionDBModel ToTransactionDBModel(ITransaction transactionToMap)
        {
            var transactionDbModel = new TransactionDBModel
            {
                Id = transactionToMap.Id,
                DateTime = transactionToMap.DateTime,
                SellerId = transactionToMap.SellerId,
                PayLater = transactionToMap.PayLater != null ? new PayLaterDTO(transactionToMap.PayLater).ToDBModel() : null,
                TransactionProducts = new List<TransactionProductDBModel>()
            };

            foreach (var transactionProduct in transactionToMap.TransactionProducts)
            {
                var transactionProductDBModel = new TransactionProductDBModel
                {
                    TransactionId = transactionDbModel.Id,
                    ProductId = transactionProduct.ProductId,
                    ProductName = transactionProduct.ProductName,
                    CostPrice = transactionProduct.CostPrice,
                    SellingPrice = transactionProduct.SellingPrice,
                    QuantityBought = transactionProduct.QuantityBought,
                };
                transactionDbModel.TransactionProducts.Add(transactionProductDBModel);
            }

            return transactionDbModel;
        }

        public static TransactionDTO ToTransactionDTO(TransactionDBModel dbObject)
        {
            PayLaterDTO? payLaterDTO = null;
            if (dbObject.PayLater != null)
            {
                payLaterDTO = new PayLaterDTO(dbObject.PayLater);
            }

            var transactionProductsDTO = new List<TransactionProductDTO>();
            foreach (var transactionProduct in dbObject.TransactionProducts)
            {
                transactionProductsDTO.Add(
                    new TransactionProductDTO
                    {
                        ProductId = transactionProduct.ProductId,
                        ProductName = transactionProduct.ProductName,
                        CostPrice = transactionProduct.CostPrice,
                        SellingPrice = transactionProduct.SellingPrice,
                        QuantityBought = transactionProduct.QuantityBought,
                    });
            }

            var transactionDTO = new TransactionDTO(
                dbObject,
                payLaterDTO,
                transactionProductsDTO
            );

            return transactionDTO;
        }

        public IEnumerable<ITransaction> GetAll()
        {
            var allTransactions = new List<ITransaction>();
            
           foreach(var transaction in _transactions.Include(t => t.TransactionProducts).Include(t => t.PayLater).ToList())
            {
                var transactionDTO = TransactionRepositoryImpl.ToTransactionDTO(transaction);
                allTransactions.Add(_transactionFactory.Reconstitute(transactionDTO));
            }

            return allTransactions;
        }

        public ITransaction? GetById(Guid id)
        {
            var transactionDbObject = _transactions.Include(t => t.TransactionProducts).Include(t => t.PayLater).SingleOrDefault(t => t.Id == id);
            if(transactionDbObject != null)
            {
                var transactionDTO = TransactionRepositoryImpl.ToTransactionDTO(transactionDbObject);
                return _transactionFactory.Reconstitute(transactionDTO);

            }
            return null;
        }

        public void Remove(Guid id)
        {
            var transactionToRemove = _transactions.Find(id);
            if(transactionToRemove != null)
            {
                _transactions.Remove(transactionToRemove);
            }

            _dbContext.SaveChanges();

        }

        public void Update(ITransaction updatedTransaction)
        {
            var transactionToUpdate = ToTransactionDBModel(updatedTransaction);
            var transactionEntry = _transactions.Entry(transactionToUpdate);
            transactionEntry.State = EntityState.Modified;

            _dbContext.SaveChanges();
            _dbContext.ChangeTracker.Clear();
        }


        public IEnumerable<ITransaction> GetTransactionsToday()
        {
            var queryResult = new List<ITransaction>();
            foreach (var transaction in _transactions.Include(t => t.TransactionProducts).Include(t => t.PayLater).Where(t => t.DateTime.Date == DateTime.Now.Date).OrderByDescending(t => t.DateTime).ToList())
            {
                queryResult.Add(_transactionFactory.Reconstitute(ToTransactionDTO(transaction)));
            }

            return queryResult;
        }
    }
}
