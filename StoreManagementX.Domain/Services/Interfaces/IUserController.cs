using StoreManagementSystemX.Domain.Aggregates.Roots.Users;

namespace StoreManagementSystemX.Domain.Services.Interfaces
{
    public interface IUserController
    {
        public void Save(User user);

        public void Delete(Guid userId);
    }
}