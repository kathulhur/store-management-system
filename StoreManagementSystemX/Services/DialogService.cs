using StoreManagementSystemX.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StoreManagementSystemX.Services
{
    public class DialogService : IDialogService
    {
        public bool ShowConfirmationDialog(string title, string message)
        {
            MessageBoxResult result = MessageBox.Show(Application.Current.MainWindow, message, title, MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                return true;

            return false;
        }

        public void ShowMessageDialog(string title, string message)
        {
            MessageBox.Show(message, title);
        }
    }
}
