using StoreManagementSystemX.Domain.Repositories.Products.Interfaces;
using StoreManagementSystemX.Services;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.ViewModels.Products;
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
using System.Windows.Shapes;

namespace StoreManagementSystemX.Views.Products
{
    /// <summary>
    /// Interaction logic for CreateProductWindow.xaml
    /// </summary>
    public partial class CreateProductWindow : Window
    {

        public CreateProductWindow(AuthContext authContext, Domain.Repositories.Products.Interfaces.IProductRepository productRepository, IBarcodeImageService barcodeImageService, Action<Guid> onSubmit)
        {
            InitializeComponent();
            this.DataContext = new CreateProductViewModel(authContext, productRepository, barcodeImageService, onSubmit, () =>
            {
                Close();
            });
        }


    }
}
