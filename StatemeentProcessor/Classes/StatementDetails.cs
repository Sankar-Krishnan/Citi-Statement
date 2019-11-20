using System;


namespace StatemeentProcessor.Classes
{
    public class StatementDetails
    {
        public DateTime Date { get; set; }
        public string To { get; set; }
        public string Desc { get; set; }
        public double Amount { get; set; }

        public PaymentMode PaymentMode { get; set; }
    }
}
