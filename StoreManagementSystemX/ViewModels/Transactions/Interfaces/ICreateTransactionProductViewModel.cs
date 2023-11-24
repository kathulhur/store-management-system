using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.Transactions.Interfaces
{
    public interface ICreateTransactionProductViewModel
    {
        public string ProductBarcode { get; }

        public string ProductName { get; }

        public decimal Price { get; }

        public int Quantity { get; set; }

        public decimal Subtotal { get; }

        public ICommand RemoveCommand { get; }
        public event EventHandler<EventArgs<ICreateTransactionProductViewModel>> ItemRemoved;

        public ICommand IncrementQuantityCommand { get; }
        public event EventHandler<EventArgs<ICreateTransactionProductViewModel>> QuantityIncremented;

        public ICommand DecrementQuantityCommand { get; }
        public event EventHandler<EventArgs<ICreateTransactionProductViewModel>> QuantityDecremented;

        public void OnDone();

    }

    public class EventArgs<T> : EventArgs
    {
        public T Item { get; set; }

        public EventArgs(T item) : base()
        {

            Item = item;
        }
    }

}
