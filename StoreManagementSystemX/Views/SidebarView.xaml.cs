using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StoreManagementSystemX.Views
{
    /// <summary>
    /// Interaction logic for Sidebar.xaml
    /// </summary>
    public partial class SidebarView : UserControl
    {
        public SidebarView()
        {
            InitializeComponent();
        }


        private MainViewModel? mainViewModel { get => Application.Current.MainWindow.DataContext as MainViewModel; }

        private void Dashboard_Button_Click(object sender, RoutedEventArgs e)
        {
            if (mainViewModel == null)
            {
                throw new NullReferenceException("MainViewModel is null!");
            }

            mainViewModel.NavigationService.NavigateTo(View.Dashboard);
        }

        private void Inventory_Button_Click(object sender, RoutedEventArgs e)
        {
            if (mainViewModel == null)
            {
                throw new NullReferenceException("MainViewModel is null!");
            }

            mainViewModel.NavigationService.NavigateTo(View.Inventory);
        }

        private void Transactions_Button_Click(object sender, RoutedEventArgs e)
        {
            if (mainViewModel == null)
            {
                throw new NullReferenceException("MainViewModel is null!");
            }

            mainViewModel.NavigationService.NavigateTo(View.Transactions);
        }
        
        private void Users_Button_Click(object sender, RoutedEventArgs e)
        {
            if (mainViewModel == null)
            {
                throw new NullReferenceException("MainViewModel is null!");
            }

            mainViewModel.NavigationService.NavigateTo(View.UserList);
        }

        private void Logout_Button_Click(object sender, RoutedEventArgs e)
        {
            if(mainViewModel == null)
            {
                throw new NullReferenceException("MainViewModel is Null");
            }
            mainViewModel.AuthenticationService.Logout();
            mainViewModel.NavigationService.NavigateTo(View.Login);
        }

        private void Pay_Later_Transactions_Button_Click(object sender, RoutedEventArgs e)
        {
            if (mainViewModel == null)
            {
                throw new NullReferenceException("MainViewModel is Null");
            }

            mainViewModel.NavigationService.NavigateTo(View.PayLaterTransactions);
        }

        private void Stock_Purchases_Button_Click(object sender, RoutedEventArgs e)
        {
            if (mainViewModel == null)
            {
                throw new NullReferenceException("MainViewModel is Null");
            }

            mainViewModel.NavigationService.NavigateTo(View.StockPurchaseList);
        }




    }
}
