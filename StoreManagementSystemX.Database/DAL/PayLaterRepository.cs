using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Database.DAL
{
    public class PayLaterRepository : IPayLaterRepository
    {

        public PayLaterRepository(Context context)
        {
            _context = context;
        }

        private readonly Context _context;

        public void Delete(Guid instanceId)
        {
            PayLater payLater = _context.PayLaters.Find(instanceId);
            _context.PayLaters.Remove(payLater);
        }

        public IEnumerable<PayLater> GetAll()
        {
            return _context.PayLaters.ToList();
        }

        public PayLater? GetById(Guid instanceId)
        {
            return _context.PayLaters.Find(instanceId);
        }

        public void Insert(PayLater newInstance)
        {
            _context.PayLaters.Add(newInstance);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
