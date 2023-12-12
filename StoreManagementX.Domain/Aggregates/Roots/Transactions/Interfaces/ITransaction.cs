﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;

namespace StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces
{
    // Aggregate

    // Aggregate root
    public interface ITransaction
    {
        public Guid SellerId { get; }

        public Guid Id { get; }

        public IReadOnlyList<ITransactionProduct> TransactionProducts { get; }

        public decimal TotalAmount { get; }

        public void AddProduct(IProduct product);

        public void RemoveProduct(IProduct product);


        
        
    }
}
