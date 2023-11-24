﻿using StoreManagementSystemX.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Database.DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {

        public User? Find(Expression<Func<User, bool>> predicate);

    }
}
