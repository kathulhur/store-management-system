using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.Users.Interfaces
{
    public interface ICreateUserViewModel
    {
        public string Username { get; }

        public ICommand SubmitCommand { get; }

        public ICommand CancelCommand { get; }

    }
}
