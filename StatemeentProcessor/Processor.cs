using System;
using System.Text.RegularExpressions;
using StatemeentProcessor.Classes;
using System.Collections.Generic;
using System.Linq;

namespace StatemeentProcessor
{
    public class Processor
    {
        public List<StatementDetails> ProcessStatement(IEnumerable<StatementRow> statement)
        {
            List<StatementDetails> details = new List<StatementDetails>();

            foreach (var row in statement)
            {
                StatementDetails detail = new StatementDetails();
                detail.Date = row.Date;

                if (row.Description.StartsWith("IMPS OUTWARD ORG UPI"))
                {
                    var entries = row.Description.Split(',');

                    detail.To = row.Description.Split(',')[0].Replace("IMPS OUTWARD ORG UPI To ", "");
                    detail.Desc = entries[entries.Length - 1];
                    detail.Amount = row.Deposits.HasValue ? row.Deposits.Value : 0;
                    detail.PaymentMode = PaymentMode.IMPS;

                    details.Add(detail);
                }
                else if (row.Description.StartsWith("IMPS OUTWARD ORG IMPS TO "))
                {
                    int refIndex = row.Description.IndexOf("REF NO:") + 9;
                    var subString = row.Description.Substring(refIndex, row.Description.Length - refIndex);
                    var desc = Regex.Replace(subString, @"^[\d-]*\s*", string.Empty);

                    refIndex = desc.LastIndexOf("-") + 1;
                    detail.To = desc.Substring(refIndex, desc.Length - refIndex);
                    refIndex = 0;
                    detail.Desc = desc.Substring(refIndex, (int)desc.LastIndexOf("-") - 1);
                    detail.Amount = row.Deposits.HasValue ? row.Deposits.Value : 0;
                    detail.PaymentMode = PaymentMode.IMPS;

                    details.Add(detail);
                }
                else if (row.Description.StartsWith("PURCHASE SUBJECT:"))
                {
                    int cardLocationIndex = row.Description.IndexOf("3005");
                    int refIndex = row.Description.LastIndexOf("Ref:");
                    int startIndex = cardLocationIndex + 4 + 16;

                    detail.To = row.Description.Substring(startIndex, refIndex - startIndex);
                    detail.Amount = row.Deposits.HasValue ? row.Deposits.Value : 0;
                    detail.PaymentMode = PaymentMode.DebitCard;

                    details.Add(detail);
                }
                else if (row.Description.StartsWith("Payment for Credit Card"))
                {
                    detail.PaymentMode = PaymentMode.CreditCardBill;
                    detail.To = "Payment for Credit Card";
                    detail.Amount = row.Deposits.HasValue ? row.Deposits.Value : 0;

                    details.Add(detail);
                }
                else if (row.Description.StartsWith("ATM WITHDRAWAL"))
                {
                    detail.PaymentMode = PaymentMode.ATMWithdrawl;
                    detail.To = "ATM WITHDRAWAL";
                    detail.Amount = row.Deposits.HasValue ? row.Deposits.Value : 0;

                    details.Add(detail);
                }
                else if (row.Description.StartsWith("STANDING INSTRN"))
                {
                    detail.PaymentMode = PaymentMode.StandingInstruction;

                    var desc = row.Description.Replace("STANDING INSTRN", string.Empty).Split("|");

                    detail.To = desc[0];
                    detail.Desc = desc[2];
                    detail.Amount = row.Deposits.HasValue ? row.Deposits.Value : 0;

                    details.Add(detail);
                }
                else
                {
                    if (row.Deposits.HasValue && row.Deposits.Value > 0)
                    {
                        detail.PaymentMode = PaymentMode.Others;
                        detail.To = row.Description;
                        detail.Amount = row.Deposits.HasValue ? row.Deposits.Value : 0;

                        details.Add(detail);
                    }
                }                
            }

            return details;
        }
    }
}
