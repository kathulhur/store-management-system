using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Services.Interfaces
{
    public interface IProductCreationService
    {

        public Guid? CreateNewProduct(AuthContext authContext);
    }
}
