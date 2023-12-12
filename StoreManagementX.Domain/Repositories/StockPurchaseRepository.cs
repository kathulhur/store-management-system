using StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases;
using StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Repositories
{
    public class StockPurchaseRepository : IRepository<IStockPurchase>
    {
        private readonly List<IStockPurchase> _stockPurchases;

        public StockPurchaseRepository()
        {
            _stockPurchases = new List<IStockPurchase>();
        }
        public void Add(IStockPurchase newEntity)
        {
            _stockPurchases.Add(newEntity);
        }

        public IEnumerable<IStockPurchase> GetAll()
        {
            return _stockPurchases.AsReadOnly();
        }

        public IStockPurchase? GetById(Guid id)
        {
            return _stockPurchases.Find(sp => sp.Id == id);
        }

        public void Remove(Guid id)
        {
            var stockPurchaseToRemove = _stockPurchases.Find(e => e.Id == id);
            if (stockPurchaseToRemove != null)
            {
                _stockPurchases.Remove(stockPurchaseToRemove);
            } else
            {
                throw new Exception($"Stock purchase with id - {id} does not exist.");
            }
        }

        public void Update(IStockPurchase updatedStockPurchase)
        {
            var stockPurchaseToUpdateIndex = _stockPurchases.FindIndex(e => e.Id == updatedStockPurchase.Id);
            if(stockPurchaseToUpdateIndex != -1)
            {
                _stockPurchases[stockPurchaseToUpdateIndex] = updatedStockPurchase;
            }
        }
    }
}
