namespace StoreManagementSystemX.Domain.Factories.StockPurchases.Interfaces
{
    public interface IStockPurchaseProductReconstitutionArgs
    {
        public Guid ProductId { get; }

        public string Barcode { get; }

        public string Name { get; }

        public decimal Price { get; }

        public int QuantityBought { get; }


    }
}