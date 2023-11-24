using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StoreManagementSystemX.ViewModels.Transactions.Interfaces;

namespace StoreManagementSystemX.ViewModels.Users.Interfaces
{
    public interface IUserRowViewModel
    {
        public Guid Id { get; }

        public string Username { get; }

        public ICommand UpdateCommand { get; }
        public event EventHandler<EventArgs<IUserRowViewModel>> UserUpdated;

        public ICommand DeleteCommand { get; }
        public event EventHandler<EventArgs<IUserRowViewModel>> UserDeleted;

    }
}
