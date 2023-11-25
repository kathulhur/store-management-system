using StoreManagementSystemX.Database.DAL;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Services;
using StoreManagementSystemX.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StoreManagementSystemX.Views.Users
{
    /// <summary>
    /// Interaction logic for CreateUserWindow.xaml
    /// </summary>
    public partial class CreateUserWindow : Window
    {
        public CreateUserWindow(AuthContext authContext, IUnitOfWorkFactory unitOfWorkFactory, Action<Guid> onCreate)
        {
            InitializeComponent();

            _viewModel = new CreateUserViewModel(authContext, unitOfWorkFactory, onCreate, () =>
            {
                Close();
            });
            DataContext = _viewModel;
        }

        private CreateUserViewModel _viewModel;

        private void Submit_Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SubmitCommand.Execute(Password_Box.Password);
        }


    }
}
