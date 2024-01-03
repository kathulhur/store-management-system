using Microsoft.EntityFrameworkCore;
using StoreManagementSystemX.Database;
using StoreManagementSystemX.Domain.Aggregates.Roots.Users.Interfaces;
using StoreManagementSystemX.Domain.Factories.Users.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Users.Interfaces;
using StoreManagementSystemX.Infrastructure.DTO;
using StoreManagementSystemX.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Infrastructure.Persistence
{
    public class UserRepositoryImpl : IUserRepository
    {
        private DbContext _context;
        private DbSet<UserDBModel> _users;
        private IUserFactory _userFactory;

        public UserRepositoryImpl(IUserFactory userFactory)
        {
            _userFactory = userFactory;
            _context = new StoreContext();
            _users = _context.Set<UserDBModel>();
        }
        public void Add(IUser newEntity)
        {
            var userDTO = new UserDTO(newEntity);

            _users.Add(userDTO.ToDBModel());
            _context.SaveChanges();
        }

        public IEnumerable<IUser> GetAll()
        {
            var allUsers = new List<IUser>();
            foreach (var user in _users)
            {
                var userDTO = new UserDTO(user);
                allUsers.Add(_userFactory.Reconstitute(userDTO));
            }

            return allUsers;
        }

        public IUser? GetById(Guid id)
        {
            var storedUser = _users.Find(id);
            if(storedUser != null)
            {
                return _userFactory.Reconstitute(storedUser);
            }

            return null;
        }

        public IUser? GetByUsernameAndPassword(string username, string password)
        {

            var matchedUser = _users.SingleOrDefault(u => u.Username == username && u.Password == password);
            if(matchedUser != null)
            {
                return _userFactory.Reconstitute(matchedUser);

            }

            return null;
        }

        public void Remove(Guid id)
        {
            var matchedUser = _users.Find(id);
            if(matchedUser != null)
            {
                _users.Remove(matchedUser);
            }
            _context.SaveChanges();
        }

        public void Update(IUser newEntity)
        {
            var userToUpdate = new UserDTO(newEntity).ToDBModel();
            _users.Entry(userToUpdate).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
