using StoreManagementSystemX.Domain.Factories.Users.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Infrastructure.Models
{
    public class UserDBModel : IUserReconstitutionArgs
    {
        public Guid Id { get; set; }

        public UserDBModel? Creator { get; set; }

        public Guid? CreatorId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
