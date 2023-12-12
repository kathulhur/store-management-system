using StoreManagementSystemX.Domain.Aggregates.Roots.Users;
using StoreManagementSystemX.Domain.Aggregates.Roots.Users.Interfaces;
using StoreManagementSystemX.Domain.Factories.Users;
using StoreManagementSystemX.Domain.Factories.Users.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Repositories
{
    public class UserRepository : IRepository<IUser>
    {
        private readonly List<User> _users;

        private readonly UserFactory _userFactory;

        public UserRepository(UserFactory userFactory)
        {
            _users = new List<User>();
        }


        public void Add(IUser newEntity)
        {
            _users.Add((User)newEntity);
        }

        public void Remove(Guid id)
        {
            var matchedUser = _users.Find(x => x.Id == id);
            if (matchedUser != null)
            {
                _users.Remove(matchedUser);
            }
        }

        public IEnumerable<IUser> GetAll()
        {
            return _users;
        }

        public IUser? GetById(Guid id)
        {
            return _users.Find(e => e.Id == id);
        }

        public void Update(IUser newEntity)
        {
            User? userToUpdate = newEntity as User;
            if (userToUpdate != null)
            {
                var matchedUserIndex = _users.FindIndex(u => u.Id == newEntity.Id);
                _users[matchedUserIndex] = userToUpdate;

            }
        }

        private class CreateUserArgs : ICreateUserArgs
        {
            public Guid CreatorId { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
