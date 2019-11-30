using System;

namespace StatemeentProcessor
{
    public enum PaymentMode
    {
        IMPS = 1,
        UPI,
        CreditCardBill,
        DebitCard,
        ATMWithdrawl,
        StandingInstruction,
        Self,
        Family,
        Others
    }
}