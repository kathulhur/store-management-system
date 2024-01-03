using StoreManagementSystemX.Domain.Repositories.Products.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Transactions.Interfaces;
using StoreManagementSystemX.Services;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.ViewModels.Transactions;
using StoreManagementSystemX.ViewModels.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

namespace StoreManagementSystemX.Views.Transactions
{
    /// <summary>
    /// Interaction logic for CreateTransactionWindow.xaml
    /// </summary>
    public partial class CreateTransactionWindow : Window
    {

        public CreateTransactionWindow(
            AuthContext authContext,
            Domain.Repositories.Transactions.Interfaces.ITransactionRepository transactionRepository, 
            Domain.Repositories.Products.Interfaces.IProductRepository productRepository, IDialogService dialogService, Action<Guid> onSave)
        {
            InitializeComponent();
            _viewModel = new CreateTransactionViewModel(authContext, transactionRepository, productRepository, dialogService, (Guid newTransactionId) =>
            {
                onSave(newTransactionId);
                Close();
            }, () =>
            {
                Close();
            });

            this.DataContext = _viewModel;

            if (PayLaterCheckBox.IsChecked == true)
            {
                CustomerNameInputGrid.Visibility = Visibility.Visible; 
            } else
            {
                CustomerNameInputGrid.Visibility = Visibility.Hidden; 

            }

            Owner = Application.Current.MainWindow;

        }

        private readonly Action<TransactionRowViewModel> _onSave;

        private readonly AuthContext _authContext;

        private readonly CreateTransactionViewModel _viewModel;


        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && sender == Barcode_TextBox)
            {
                try
                {
                    _viewModel.AddProduct();
                } catch
                {
                    MessageBox.Show(this, "Product not found!");
                }
                e.Handled = true;
            }
        }

        private void PayLater_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CustomerNameInputGrid.Visibility = Visibility.Visible;
        }

        private void PayLater_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CustomerNameInputGrid.Visibility = Visibility.Hidden;

        }
    }
}
