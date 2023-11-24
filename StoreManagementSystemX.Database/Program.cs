using StoreManagementSystemX.Database.DAL;
using StoreManagementSystemX.Database.Models;
using System;
using System.Linq;
using System.Reflection.Metadata;



using var unitOfWork = new UnitOfWork();



// Note: This sample requires the database to be created before running.


// Read data
Console.WriteLine("Querying for a user");
var users = unitOfWork.UserRepository.GetAll();


foreach (var user in users)
{
    Console.WriteLine(user);
}


var products = unitOfWork.ProductRepository.GetAll();

foreach(var product in products)
{

    Console.WriteLine(product);
}


var transactions = unitOfWork.TransactionRepository.GetAll();

foreach (var transaction in transactions)
{
    Console.WriteLine(transaction);
}


var transactionProducts = unitOfWork.TransactionProductRepository.GetAll();

foreach(var transactionProduct in transactionProducts)
{
    Console.WriteLine(transactionProduct);
}

var stockPurchases = unitOfWork.StockPurchaseRepository.GetAll();

foreach (var stockPurchase in stockPurchases)
{
    Console.WriteLine(stockPurchase);
}

unitOfWork.StockPurchaseRepository.Insert(new StockPurchase { MadeById = users.First().Id, DateTime = DateTime.Now });

Console.WriteLine("Tests");
stockPurchases = unitOfWork.StockPurchaseRepository.GetAll();
unitOfWork.Save();

foreach (var stockPurchase in stockPurchases)
{
    Console.WriteLine(stockPurchase);
}