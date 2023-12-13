namespace StoreManagementSystemX.Domain.Factories.Products.Interfaces
{
    public interface ICreateProductArgs
    {
        Guid CreatorId { get; }

        string? Barcode { get; }

        string Name { get; }

        decimal CostPrice { get; }

        decimal SellingPrice { get; }
    }
}