using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Services;
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

        public CreateProductWindow(AuthContext authContext, IUnitOfWork unitOfWork, Action<Guid> onSubmit)
        {
            InitializeComponent();
            this.DataContext = new CreateProductViewModel(authContext, unitOfWork, onSubmit, () =>
            {
                Close();
            });
        }


    }
}
