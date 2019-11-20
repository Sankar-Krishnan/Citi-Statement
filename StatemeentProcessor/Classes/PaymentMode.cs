using System;

namespace StatemeentProcessor {
    public enum PaymentMode {
        IMPS = 1,
        CreditCardBill,
        DebitCard,
        ATMWithdrawl,
        StandingInstruction,
        Others
    }
}