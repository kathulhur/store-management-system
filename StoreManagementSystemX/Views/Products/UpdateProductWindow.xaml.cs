using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace StoreManagementSystemX.Views.Products
{
    /// <summary>
    /// Interaction logic for UpdateProductWindow.xaml
    /// </summary>
    public partial class UpdateProductWindow : Window
    {
        public UpdateProductWindow(Guid productId, IUnitOfWork unitOfWork, Action<ProductUpdateServiceResponse> onAction)
        {
            InitializeComponent();
            DataContext = new UpdateProductViewModel(productId, unitOfWork, () =>
            {
                Close();
            }, onAction);
        }





    }
}
