using StoreManagementSystemX.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Database.DAL.Interfaces;

public interface IStockPurchaseRepository : IRepository<StockPurchase>
{
    public StockPurchase? GetById(Guid stockPurchaseId, string includeProperties = "");
}
