using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.Products.Interfaces
{
    public interface IUpdateProductViewModel
    {
        public string Name { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SellingPrice { get; set; }

        public ICommand CancelCommand { get; }

        public ICommand SubmitCommand { get; }



    }
}
