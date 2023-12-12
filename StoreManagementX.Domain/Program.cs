using StoreManagementSystemX.Domain.Factories.Products;
using StoreManagementSystemX.Domain.Factories.Products.Interfaces;
using StoreManagementSystemX.Domain.Factories.StockPurchases;
using StoreManagementSystemX.Domain.Factories.Transactions;
using StoreManagementSystemX.Domain.Factories.Users;
using StoreManagementSystemX.Domain.Factories.Users.Interfaces;


IUserFactory userFactory = new UserFactory();


var newUser = userFactory.Create(new UserViewModel { Username = "hello", Password = "world" });
var newUser1 = newUser.UserFactory.Create(new UserViewModel { CreatorId = newUser.Id, Username = "username", Password = "password" });

Console.WriteLine(newUser);
Console.WriteLine(newUser1);


newUser1.Username = "haha";
Console.WriteLine(newUser1);


var product1 = newUser.ProductFactory.Create(new CreateProductViewModel
{
    CreatorId = newUser.Id,
    Name = "Product 1",
    CostPrice = 10,
    SellingPrice = 20,
    InStock = 5
});


Console.WriteLine(product1);
product1.SellingPrice = 43;
Console.WriteLine(product1);


// Transaction Tests
var transaction = newUser.TransactionFactory.Create(newUser.Id);
Console.WriteLine(transaction);
Console.WriteLine(product1);


transaction.AddProduct(product1);
Console.WriteLine(transaction);
Console.WriteLine(product1);


transaction.AddProduct(product1);
Console.WriteLine(transaction);
Console.WriteLine(product1);


transaction.AddProduct(product1);
Console.WriteLine(transaction);
Console.WriteLine(product1);


// Stock Purchase tests
Console.WriteLine("\n\n -----Stock Purchase tests----- \n\n");
var stockPurchase = newUser.StockPurchaseFactory.Create(newUser.Id);
Console.WriteLine(stockPurchase);
Console.WriteLine(product1);


stockPurchase.AddProduct(product1);
Console.WriteLine(stockPurchase);
Console.WriteLine(product1);


stockPurchase.AddProduct(product1);
Console.WriteLine(stockPurchase);
Console.WriteLine(product1);


stockPurchase.AddProduct(product1);
Console.WriteLine(stockPurchase);
Console.WriteLine(product1);

Console.WriteLine("\n\n ------------------------ \n\n");

class UserViewModel : ICreateUserArgs
{
    public Guid CreatorId { get; set; }
    
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}

class CreateProductViewModel : ICreateProductArgs
{
    public Guid CreatorId { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal CostPrice { get; set; }

    public decimal SellingPrice { get; set; }

    public int InStock { get; set; }
}



