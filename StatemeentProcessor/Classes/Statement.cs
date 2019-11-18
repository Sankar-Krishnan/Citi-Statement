//Date,Description,Withdrawals,Deposits,Balance
using System;

namespace StatemeentProcessor.Classes {
    public class StatementRow {
        public DateTime Date {get;set;}
        public string Description {get;set;}
        public decimal Withdrawals {get;set;}
        public decimal Deposits {get;set;}
        public decimal Balance {get;set;}
    }
}
