using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.Products.Interfaces
{
    public interface ICreateProductViewModel
    {


        public string Barcode { get; set; }

        public string Name { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SellingPrice { get; set; }


        public ICommand SubmitCommand { get; }

        public ICommand CancelCommand { get; }


    }
}
