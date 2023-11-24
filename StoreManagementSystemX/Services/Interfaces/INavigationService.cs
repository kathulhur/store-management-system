using StoreManagementSystemX.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Services.Interfaces
{
    public enum View
    {
        Login,
        Dashboard,
        Inventory,
        Transactions,
        PayLaterTransactions,
        UserList,
        StockPurchaseList
    }
    public interface INavigationService
    {

        public BaseViewModel CurrentViewModel {  get; }

        public void NavigateTo(View view);

        public void Exit();
    }
}
