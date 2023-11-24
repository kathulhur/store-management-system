using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.Users.Interfaces
{
    public interface IUserListViewModel
    {

        public ObservableCollection<IUserRowViewModel> Users { get; }

        public ICommand NewUserCommand { get; }
    }
}
