namespace StoreManagementSystemX.Domain.Factories.Users.Interfaces
{
    public interface IUserReconstitutionArgs
    {
        Guid Id { get; }

        Guid? CreatorId { get; }

        string Username { get; }

        string Password { get; }
    }
}