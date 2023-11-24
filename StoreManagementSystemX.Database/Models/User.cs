using StoreManagementSystemX.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Database.Models
{
    public class User
    {

        //public User(string username, string password)
        //{
        //    Username = username;
        //    Password = password;
        //} 

        public Guid Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public Guid? CreatedById {  get; set; }

        public User? CreatedBy { get; set; }

        public ICollection<Transaction> TransactionsHandled { get; set; } = new List<Transaction>();

        public ICollection<User> UsersCreated { get; set; } = new List<User>();

        public ICollection<Product> ProductsCreated { get; set; } = new List<Product>();

        public ICollection<StockPurchase> StockPurchasesMade { get; set; } = new List<StockPurchase>();

        public override string ToString()
        {
            return $"ID: {Id}, Username: {Username}, Password: {Password}, Created by: {CreatedBy?.Username}";
        }
    }
}
