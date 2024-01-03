using StoreManagementSystemX.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases.StockPurchase;

namespace StoreManagementSystemX.Infrastructure.Models
{
    public class StockPurchaseDBModel
    {

        public Guid Id { get; set; }

        public Guid StockManagerId { get; set; }

        public UserDBModel StockManager { get; set; } = null!;

        public DateTime DateTime { get; set; }

        public IList<StockPurchaseProductDBModel> StockPurchaseProducts { get; } = new List<StockPurchaseProductDBModel>();

        public IList<ProductDBModel> Products { get; } = new List<ProductDBModel>();
    }
}
