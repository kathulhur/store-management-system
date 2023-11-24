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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private LoginViewModel? LoginViewModel { get => DataContext as LoginViewModel; }

        private MainViewModel? MainViewModel { get => Application.Current.MainWindow.DataContext as MainViewModel; }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            if(LoginViewModel != null)
            {
                if(LoginViewModel.Login(Password_Field.Password) != null)
                {
                    MainViewModel.NavigationService.NavigateTo(Services.Interfaces.View.Dashboard);
                } else
                {
                    MessageBox.Show("Invalid credentials.");
                }

                return;
            }

            throw new Exception("LoginViewModel is null");
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.NavigationService.Exit();
        }
    }
}
