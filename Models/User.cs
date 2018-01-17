using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankApp.Core;

namespace BankApp.Models
{
    public class User
    {
        private string userName;
        private List<BankAccount> userAccounts;

        public User(string newUserName)
        {
            userName = newUserName;
            userAccounts = new List<BankAccount>();
        }

        public bool AddAccount(BankAccount newAccount)
        {
            userAccounts.Add(newAccount);
            return true;
        }

        public List<BankAccount> GetUserAccounts()
        {
            return userAccounts;
        }

        public User GetUser()
        {
            return this;
        }
    }
}