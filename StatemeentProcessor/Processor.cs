using System;
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

            Console.WriteLine(statement.Count());
            foreach(var row in statement) {
                StatementDetails detail = new StatementDetails();
                if(row.Description.StartsWith("IMPS OUTWARD ORG UPI")) {
                    detail.To = row.Description.Split(',')[0].Replace("IMPS OUTWARD ORG UPI", "");
                    var entries = row.Description.Split(',');
                    detail.Desc = entries[entries.Length-1];
                    detail.Amount = row.Deposits;
                }

                details.Add(detail);
            }

            return details;
        }
    }
}
