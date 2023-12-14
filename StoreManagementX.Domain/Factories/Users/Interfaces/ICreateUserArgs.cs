namespace StoreManagementSystemX.Domain.Factories.Users.Interfaces
{
    public interface ICreateUserArgs
    {
        Guid CreatorId { get; }

        string Username { get; }

        string Password { get; }
    }
}