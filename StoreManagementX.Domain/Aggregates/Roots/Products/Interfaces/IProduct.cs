namespace StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces
{
    public interface IProduct
    {
        public Guid Id { get; }

        public string Barcode { get; }

        public string Name { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SellingPrice { get; set; }

        public int InStock { get; set; }

        public Guid CreatorId { get; }
    }
}