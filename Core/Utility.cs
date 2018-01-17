using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankApp.Core
{
    public class Utility
    {
        private int nextBankID;
        private int nextAccountID;
        private int nextUserID;

        public Utility()
        {
            nextBankID = 1;
            nextAccountID = 1;
            nextUserID = 1;
        }

        public int GetNextBankID()
        {
            return nextBankID;
        }

        public int GetNextAccountID()
        {
            return nextAccountID;
        }

        public int GetNextUserID()
        {
            return nextUserID;
        }
    }
}