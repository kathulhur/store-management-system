using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Database.DAL
{
    public class UserRepository : IUserRepository, IDisposable
    {

        private readonly Context _context;
        public UserRepository(Context context)
        {
            _context = context;
        }

        public void Delete(Guid userId)
        {
            User user = _context.Users.Find(userId);
            _context.Users.Remove(user);
        }

        public User? Find(Expression<Func<User, bool>> predicate)
        {
            return _context.Users.Where(predicate).FirstOrDefault();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User? GetById(Guid userId)
        {
            return _context.Users.Find(userId);
        }

        public void Insert(User user)
        {
            _context.Users.Add(user);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
