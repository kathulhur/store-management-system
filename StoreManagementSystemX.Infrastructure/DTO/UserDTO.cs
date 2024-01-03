using StoreManagementSystemX.Domain.Aggregates.Roots.Users.Interfaces;
using StoreManagementSystemX.Domain.Factories.Users.Interfaces;
using StoreManagementSystemX.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Infrastructure.DTO
{
    public class UserDTO : IUserReconstitutionArgs
    {

        public UserDTO(IUser user)
        {
            Id = user.Id;
            CreatorId = user.CreatorId;
            Username = user.Username;
            Password = user.Password;
        }

        public UserDTO(UserDBModel user)
        {
            Id = user.Id;
            CreatorId = user.CreatorId;
            Username = user.Username;
            Password = user.Password;
        }

        public Guid Id { get; set; }

        public Guid? CreatorId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public UserDBModel ToDBModel()
        {
            return new UserDBModel
            {
                Id = Id,
                CreatorId = CreatorId,
                Username = Username,
                Password = Password,
            };
        }
    }
}
