using StoreManagementSystemX.Database.Models;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;
using StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.StockPurchases.Interfaces
{
    public interface ICreateStockPurchaseProductViewModel
    {
        public string Barcode { get; }

        public string Name { get; }

        public int Quantity { get; }

        public decimal Price { get; }

        public decimal TotalPrice { get; }

        public ICommand RemoveCommand { get; }

        public ICommand IncrementQuantityCommand { get; }

        public ICommand DecrementQuantityCommand { get; }

    }
}
