using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankApp.Core
{
    public class GlobalVariables
    {
        internal static object AccountTypes;
    }
    public enum AccountTypes
    {
        Checking,
        CorporateInvestment,
        IndividualInvestment
    }

    public enum TransactionTypes
    {
        Withdrawal,
        Deposit,
        Transfer
    }
}