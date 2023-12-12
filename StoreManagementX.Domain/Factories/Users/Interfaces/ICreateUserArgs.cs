namespace StoreManagementSystemX.Domain.Factories.Users.Interfaces
{
    public interface ICreateUserArgs
    {
        Guid CreatorId { get; }

        string Username { get; set; }

        string Password { get; set; }
    }
}