//using StoreManagementX.Commons.Products;
//using StoreManagementX.Commons.Products.Interfaces;
//using StoreManagementX.Domain.Models.Products.Interfaces;
//using StoreManagementX.Domain.Services.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.InteropServices.ObjectiveC;
//using System.Text;
//using System.Threading.Tasks;

//namespace StoreManagementX.Domain.Models.Products
//{
//    public class Product
//    {
//        // entity that does not have an ID is an unsaved entity
//        private Product(Guid id, string name, decimal costPrice, decimal sellingPrice)
//        {
//            Id = id;
//            _name = name;
//            _costPrice = costPrice;
//            _sellingPrice = sellingPrice;

//            _inStock = 0;
//            _newName = string.Empty;
//            _newCostPrice = -1;
//            _newSellingPrice = -1;
//        }


//        public Guid Id { get; }

//        private string? _newName;
//        private string _name;
//        public string Name
//        {
//            get => _name;
//            set
//            {
//                if (!string.IsNullOrEmpty(value))
//                {
//                    if (value == _name)
//                    {
//                        _newName = null;
//                    }
//                    else
//                    {
//                        _newName = value;
//                    }
//                }
//            }
//        }
//        private bool NameNeedsUpdate()
//            => _newName != string.Empty;

//        private decimal? _newCostPrice;
//        private decimal _costPrice;
//        public decimal CostPrice
//        {
//            get => _sellingPrice;
//            set
//            {
//                if (value < 0)
//                {
//                    throw new ArgumentOutOfRangeException("Cost price cannot be negative");
//                }
//                else
//                {
//                    if (value != _costPrice)
//                    {
//                        _newCostPrice = value;
//                    }
//                    else
//                    {
//                        _newCostPrice = null;
//                    }
//                }
//            }
//        }
//        private bool CostPriceNeedsUpdate()
//            => _newCostPrice != -1;

//        private decimal? _newSellingPrice;
//        private decimal _sellingPrice;
//        public decimal SellingPrice
//        {
//            get => _sellingPrice;
//            set
//            {
//                if (value < 0)
//                {
//                    throw new ArgumentOutOfRangeException("Selling price cannot be negative");
//                }
//                else
//                {
//                    if (value != _sellingPrice)
//                    {
//                        _newSellingPrice = value;
//                    }
//                    else
//                    {
//                        _newSellingPrice = null;
//                    }
//                }
//            }
//        }

//        private bool SellingPriceNeedsUpdate()
//            => _sellingPrice != 0;

//        private int _inStock;
//        public int InStock { get; set; }

//        public bool NeedsUpdate()
//            => _newName != string.Empty;

//        public Product CreateNewProductAndSave(IProductController productController, string name, decimal costPrice, decimal sellingPrice)
//        {
//            if (costPrice < 0)
//                throw new ArgumentOutOfRangeException("Cost price cannot be negative");

//            if (sellingPrice < 0)
//                throw new ArgumentOutOfRangeException("Cost price cannot be negative");

//            if (name == "")
//                throw new ArgumentException("Name cannot be empty string");

//            Product newProduct = new Product(Guid.NewGuid(), name, costPrice, sellingPrice);

//            Create(productController);
//            return newProduct;
//        }

//        private void Create(IProductController productController)
//        {

//        }

//        public void Delete(IProductController productController)
//        {
//            productController.Delete(Id);
//        }

//        private IUpdateProduct BuildUpdateProductObject()
//        {

//            var builder = new UpdateProductBuilder();
//            var propertiesToUpdate = new List<KeyValuePair<string, object>>();
//            if (NameNeedsUpdate())
//            {
//                builder.SetName(_newName);
//            }

//            if (CostPriceNeedsUpdate())
//            {
//                builder.SetCostPrice(_newCostPrice.GetValueOrDefault());
//            }

//            if (SellingPriceNeedsUpdate())
//            {
//                builder.SetSellingPrice(_newSellingPrice.GetValueOrDefault());
//            }

//            return builder.Build();
//        }

//        public void Update(IProductController productController)
//        {
//            if (NeedsUpdate())
//            {
                
//            }
            
//        }


//    }
//}
