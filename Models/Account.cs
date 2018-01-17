using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankApp.Core;

namespace BankApp.Models
{
    public class BankAccount
    {
        private User owner;

        private decimal balance;

        private decimal withdrawLimit;

        private AccountTypes accountType;

        public BankAccount(User newOwner, AccountTypes newAccountType)
        {
            owner = newOwner;
            balance = 0;
            accountType = newAccountType;
            if (accountType == AccountTypes.IndividualInvestment)
            {
                withdrawLimit = 1000;
            }
            else
            {
                withdrawLimit = 0;
            }
        }

        public Transaction ProcessWithdrawal(Transaction currentTransaction)
        {
            if (currentTransaction.GetAccount().GetAccountType() == AccountTypes.IndividualInvestment && currentTransaction.GetAmount() > withdrawLimit)
            {
                currentTransaction.SetError(true);
                var errorReason = "Individual Investment account may not have more than $" + withdrawLimit + " withdrawn at a time.  You tried to withdraw $" + currentTransaction.GetAmount() + ".";
                currentTransaction.SetErrorReason(errorReason);
                return currentTransaction;
            }

            if (balance < currentTransaction.GetAmount())
            {
                currentTransaction.SetError(true);
                var errorReason = "You do not have the funds in your account to withdraw $" + currentTransaction.GetAmount() + ".";
                currentTransaction.SetErrorReason(errorReason);
                return currentTransaction;
            }

            try
            {
                currentTransaction.SetOldBlanace(balance);
                balance -= currentTransaction.GetAmount();
                currentTransaction.SetNewBalance(balance);
                currentTransaction.SetError(false);
                var errorReason = "Withdrawl of " + currentTransaction.GetAmount() + " was successful.";
                currentTransaction.SetErrorReason(errorReason);
            }
            catch
            {
                currentTransaction.SetError(true);
                var errorReason = "An unspecified error occurred when performing a withdrawal.";
                currentTransaction.SetErrorReason(errorReason);
                return currentTransaction;
            }

            return currentTransaction;
        }

        public Transaction ProcessDeposit(Transaction currentTransaction)
        {
            try
            {
                currentTransaction.SetOldBlanace(balance);
                balance += currentTransaction.GetAmount();
                currentTransaction.SetNewBalance(balance);
                currentTransaction.SetError(false);
                var errorReason = "Deposit of " + currentTransaction.GetAmount() + " was successful.";
                currentTransaction.SetErrorReason(errorReason);
            }
            catch
            {
                currentTransaction.SetError(true);
                var errorReason = "An unspecified error occurred when performing a deposit.";
                currentTransaction.SetErrorReason(errorReason);
                return currentTransaction;
            }

            return currentTransaction;
        }

        public Transaction ProcessTransfer(Transaction currentTransaction)
        {
            if (currentTransaction.GetAccount().GetAccountType() == AccountTypes.IndividualInvestment && currentTransaction.GetAmount() > withdrawLimit)
            {
                currentTransaction.SetError(true);
                var errorReason = "Individual Investment account may not have more than $" + withdrawLimit + " withdrawn at a time.  You tried to withdraw $" + currentTransaction.GetAmount() + ".";
                currentTransaction.SetErrorReason(errorReason);
                return currentTransaction;
            }

            if (balance < currentTransaction.GetAmount())
            {
                currentTransaction.SetError(true);
                var errorReason = "You do not have the funds in your account to withdraw $" + currentTransaction.GetAmount() + ".";
                currentTransaction.SetErrorReason(errorReason);
                return currentTransaction;
            }

            if (currentTransaction.GetTransferToAccount() == null)
            {
                currentTransaction.SetError(true);
                var errorReason = "There was a problem accessing the transfer to account.";
                currentTransaction.SetErrorReason(errorReason);
                return currentTransaction;
            }

            try
            {
                var transferFrom = this.ProcessWithdrawal(currentTransaction);

                Transaction transferTo = new Transaction(owner, currentTransaction.GetTransferToAccount(), TransactionTypes.Deposit, currentTransaction.GetAmount());

                currentTransaction.SetOldBlanace(transferFrom.GetOldBalance());
                currentTransaction.SetNewBalance(transferFrom.GetNewBalance());
                currentTransaction.SetTransferToOldBlanace(transferTo.GetOldBalance());
                currentTransaction.SetTransferToNewBalance(transferTo.GetNewBalance());
            }
            catch
            {
                currentTransaction.SetError(true);
                var errorReason = "An unspecified error occurred when performing a transfer.";
                currentTransaction.SetErrorReason(errorReason);
                return currentTransaction;
            }

            return currentTransaction;
        }

        public User GetOwner()
        {
            return owner;
        }

        public decimal GetWithdrawalLimit()
        {
            return withdrawLimit;
        }
   
        public AccountTypes GetAccountType()
        {
            return accountType;
        }

        public decimal GetAccountBalance()
        {
            return balance;
        }

    }
}