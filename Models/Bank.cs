using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankApp.Models
{
    public class Bank
    {
        private string bankName;
        private List<BankAccount> bankAccounts;

        public Bank(string newBankName)
        {
            bankName = newBankName;
            bankAccounts = new List<BankAccount>();
        }

        public string GetBankName(int ID)
        {
            return bankName;
        }

        public void AddAccountToBank(BankAccount newAccount)
        {
            bankAccounts.Add(newAccount);
        }
       
    }
}