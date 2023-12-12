namespace StoreManagementSystemX.Domain.Factories.Products.Interfaces
{
    public interface ICreateProductArgs
    {
        Guid CreatorId { get; }

        string Name { get; }

        decimal CostPrice { get; }

        decimal SellingPrice { get; }

        int InStock { get; }
    }
}