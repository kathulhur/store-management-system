using static StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases.StockPurchase;

namespace StoreManagementSystemX.Domain.Factories.StockPurchases.Interfaces
{
    public interface IStockPurchaseReconstitutionArgs
    {
        public Guid StockManagerId { get; }

        public Guid Id { get; }

        public DateTime DateTime { get; }

        public IEnumerable<IStockPurchaseProductReconstitutionArgs> StockPurchaseProducts { get; }
    }
}