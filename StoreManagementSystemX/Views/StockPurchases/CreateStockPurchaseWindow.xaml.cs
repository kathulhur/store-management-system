using StoreManagementSystemX.Domain.Repositories.Products.Interfaces;
using StoreManagementSystemX.Domain.Repositories.StockPurchases.Interfaces;
using StoreManagementSystemX.Services;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.ViewModels.StockPurchases;
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

namespace StoreManagementSystemX.Views.StockPurchases;

/// <summary>
/// Interaction logic for CreateStockPurchaseWindow.xaml
/// </summary>
public partial class CreateStockPurchaseWindow : Window
{
    public CreateStockPurchaseWindow(AuthContext authContext, Domain.Repositories.StockPurchases.Interfaces.IStockPurchaseRepository stockPurchaseRepository, Domain.Repositories.Products.Interfaces.IProductRepository productRepository, IDialogService dialogService, Action<Guid> onAdd)
    {
        InitializeComponent();
        ViewModel = new CreateStockPurchaseViewModel(authContext, stockPurchaseRepository, productRepository, dialogService, (Guid newStockPurchaseId) =>
        {
            onAdd(newStockPurchaseId);
            Close();
        }, () =>
        {
            Close();
        });
        DataContext = ViewModel;
    }


    private CreateStockPurchaseViewModel ViewModel { get; }


    private void Barcode_TextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            ViewModel.AddProduct();
        }
    }
}
