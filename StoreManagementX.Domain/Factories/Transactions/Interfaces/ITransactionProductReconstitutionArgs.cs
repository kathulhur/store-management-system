namespace StoreManagementSystemX.Domain.Factories.Transactions.Interfaces
{
    public interface ITransactionProductReconstitutionArgs
    {
        public Guid ProductId { get; }

        public string ProductName { get; }

        public decimal CostPrice { get; }

        public decimal SellingPrice { get; }

        public int QuantityBought { get; }
    }
}