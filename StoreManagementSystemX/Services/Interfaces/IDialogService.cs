using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StoreManagementSystemX.Services.Interfaces
{
    public interface IDialogService
    {

        public bool ShowConfirmationDialog(string title, string message);

        public void ShowMessageDialog(string title, string message);
    }
}
