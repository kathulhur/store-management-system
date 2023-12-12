using Microsoft.EntityFrameworkCore;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Database.DAL
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {

        public ProductRepository(Context context) : base(context) { }

        public Product? GetByBarcode(string barcode)
            => Get(p => p.Barcode == barcode).FirstOrDefault();

    }
}
