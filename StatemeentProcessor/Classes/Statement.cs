//Date,Description,Withdrawals,Deposits,Balance
using System;

namespace StatemeentProcessor.Classes {
    public class StatementRow {
        public DateTime Date {get;set;}
        public string Description {get;set;}
        public double? Withdrawals {get;set;}
        public double? Deposits {get;set;}
        public double? Balance {get;set;}

        // public decimal GetWithdrawals {
        //     get{
        //         return Convert.ToDecimal(Withdrawals.Replace(",",string.Empty));
        //     }
        // }

        // public decimal GetDeposits {
        //     get {
        //         return Convert.ToDecimal(Deposits.Replace(",", string.Empty));
        //     }
        // }

        // public decimal GetBalance {
        //     get {
        //         return Convert.ToDecimal(Balance.Replace(",", string.Empty));
        //     }
        // }
    }
}
