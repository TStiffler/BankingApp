using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankApp.Core;

namespace BankApp.Models
{
    public class Transaction
    {
        private User user;
        private decimal oldBalance;
        private decimal newBalance;
        private BankAccount account;
        private TransactionTypes transactionType;
        private decimal amount;
        private bool error;
        private string errorReason;

        // Used only for transfers
        private BankAccount transferToAccount;
        private decimal transferToOldBalance;
        private decimal transferToNewBalance;

        public Transaction(User transUser, BankAccount transAccount, TransactionTypes transType, decimal transAmount, BankAccount transferTo = null)
        {
            bool validAccount = false;

            user = transUser;

            if (user == transAccount.GetOwner())
            {

                foreach (BankAccount userAccount in transUser.GetUserAccounts())
                {
                    if (userAccount == transAccount)
                    {
                        validAccount = true;
                    }
                }

                if (validAccount == true)
                {
                    oldBalance = 0;
                    newBalance = 0;
                    account = transAccount;
                    transactionType = transType;
                    amount = transAmount;
                    error = true;
                    errorReason = "Error during transaction setup.  Transaction not processed.";
                    transferToAccount = transferTo;

                    if (transactionType == TransactionTypes.Withdrawal)
                    {
                        ProcessWithdrawal();
                    }
                    else if (transactionType == TransactionTypes.Deposit)
                    {
                        ProcessDeposit();
                    }
                    else if (transactionType == TransactionTypes.Transfer)
                    {
                        ProcessTransfer();
                    }
                }
            }
        }

        private bool ProcessWithdrawal()
        {
            bool success = false;

            Transaction newWithdrawal = account.ProcessWithdrawal(this);
            if (newWithdrawal.error)
            {
                success = false;
            }
            else
            {
                success = true;
            }

            if (success)
            {
                oldBalance = newWithdrawal.GetOldBalance();
                newBalance = newWithdrawal.GetNewBalance();
                error = false;
                errorReason = "Withdrawal processed successfully.";
            }
            else
            {
                error = true;
                errorReason = newWithdrawal.GetTransactionErrorReason();
            }

            return success;
        }

        private bool ProcessTransfer()
        {
            bool success = false;

            Transaction newWithdrawal = account.ProcessTransfer(this);
            if (newWithdrawal.error)
            {
                success = false;
            }
            else
            {
                success = true;
            }

            if (success)
            {
                oldBalance = newWithdrawal.GetOldBalance();
                newBalance = newWithdrawal.GetNewBalance();
                error = false;
                errorReason = "Transfer processed successfully.";
            }
            else
            {
                error = true;
                errorReason = newWithdrawal.GetTransactionErrorReason();
            }

            return success;
        }

        private bool ProcessDeposit()
        {
            bool success = false;

            Transaction newDeposit = account.ProcessDeposit(this);
            if (newDeposit.error)
            {
                success = false;
            }
            else
            {
                success = true;
            }

            if (success)
            {
                oldBalance = newDeposit.GetOldBalance();
                newBalance = newDeposit.GetNewBalance();
                error = false;
                errorReason = "Deposit processed successfully.";
            }
            else
            {
                error = true;
                errorReason = newDeposit.GetTransactionErrorReason();
            }

            return success;
        }

        public decimal GetOldBalance()
        {
            return oldBalance;
        }

        public decimal GetNewBalance()
        {
            return newBalance;
        }

        public BankAccount GetAccount()
        {
            return account;
        }

        public TransactionTypes GetTransactionType()
        {
            return transactionType;
        }

        public decimal GetAmount()
        {
            return amount;
        }

        public void SetOldBlanace(decimal updatedOldBalance)
        {
            oldBalance = updatedOldBalance;
        }

        public void SetNewBalance(decimal updatedNewBalance)
        {
            newBalance = updatedNewBalance;
        }

        public void SetError(bool updatedError)
        {
            error = updatedError;
        }

        public void SetErrorReason(string updatedErrorReason)
        {
            errorReason = updatedErrorReason;
        }

        public bool GetTransactionError()
        {
            return error;
        }

        public string GetTransactionErrorReason()
        {
            return errorReason;
        }

        public BankAccount GetTransferToAccount()
        {
            return transferToAccount;
        }

        public decimal GetTransferToOldBalance()
        {
            return transferToOldBalance;
        }

        public decimal GetTransferToNewBalance()
        {
            return transferToNewBalance;
        }

        public void SetTransferToOldBlanace(decimal updatedOldBalance)
        {
            transferToOldBalance = updatedOldBalance;
        }

        public void SetTransferToNewBalance(decimal updatedNewBalance)
        {
            transferToNewBalance = updatedNewBalance;
        }
    }
}