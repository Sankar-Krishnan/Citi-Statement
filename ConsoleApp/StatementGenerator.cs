using System;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using StatemeentProcessor;
using StatemeentProcessor.Classes;
using CsvHelper;

namespace ConsoleApp
{
    internal class StatementGenerator
    {
        public void StartProcessing()
        {
            string directoryName = @"/Users/shankar/Documents/Personal Documents/Banking/Statements";
            var files = System.IO.Directory.GetFiles(directoryName, "*.csv").Where(a => a.Contains("Savings_Stmt_Sep_Nov.csv"));

            List<StatementDetails> statementResults = new List<StatementDetails>();

            foreach (var file in files)
            {
                var statement = ParseCSV(file);
                var temp = statement.ToList();
                Processor processor = new Processor();
                statementResults = processor.ProcessStatement(statement);
            }

            foreach (var s in statementResults.OrderByDescending(a => (int)a.PaymentMode))
            {

                //Console.WriteLine(string.Format("To: {0}, Desc: {1}, Amt: {2}", s.To, s.Desc, s.Amount));

                // if (s.PaymentMode == PaymentMode.IMPS) {
                //     //Console.WriteLine(string.Format("To: {0}, Desc: {1}, Amt: {2}", s.To, s.Desc, s.Amount));
                // }
                // else if( s.PaymentMode == PaymentMode.DebitCard) {
                //     Console.WriteLine(string.Format("To: {0}, Amt: {1}", s.To, s.Amount));
                // }
                // else if( s.PaymentMode == PaymentMode.CreditCardBill) {
                //     Console.WriteLine(string.Format("To: {0}, Amt: {1}", s.To, s.Amount));
                // }
            }

            double totalIMPS = statementResults.Where(a => a.PaymentMode == PaymentMode.IMPS).Select(a => a.Amount).Sum();
            double totalCard = statementResults.Where(a => a.PaymentMode == PaymentMode.DebitCard).Select(a => a.Amount).Sum();
            double others = statementResults.Where(a => a.PaymentMode == PaymentMode.Others).Select(a => a.Amount).Sum();

            Console.WriteLine("Total IMPS : {0}", totalIMPS.ToString("##.##"));
            Console.WriteLine("Total Card: {0}", totalCard.ToString("##.##"));
            Console.WriteLine("Other: {0}", others);

            using (var writer = new StreamWriter(@"/Users/shankar/Documents/Personal Documents/Banking/Statements/output/output.csv"))
            {
                using (var csv = new CsvWriter(writer))
                {
                    csv.WriteRecords(statementResults);
                }
            }
        }

        private IEnumerable<StatementRow> ParseCSV(string file)
        {
            // using (var reader = new StreamReader(file)) {
            //     using (var csv = new CsvReader(reader)) {
            //         csv.Configuration.HasHeaderRecord = true;

            //         var records = csv.GetRecords<StatementRow>();
            //         return records.ToList();
            //     }
            // }

            List<StatementRow> statement = new List<StatementRow>();

            using (CsvReader reader = new CsvReader(file))
            {
                foreach (string[] values in reader.RowEnumerator)
                {
                    StatementRow row = new StatementRow();
                    row.Date = Convert.ToDateTime(DateTime.ParseExact(values[0], "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    row.Description = values[1];
                    row.Deposits = values[2] != "" ? CleanNumericData(values[2].Replace(",", string.Empty)) : 0.0;
                    row.Withdrawals = values[3] != "" ? CleanNumericData(values[3].Replace(",", string.Empty)) : 0.0;
                    row.Balance = values[4] != "" ? CleanNumericData(values[4].Replace(",", string.Empty)) : 0.0;

                    statement.Add(row);
                    //Console.WriteLine("Row {0} has {1} values.", reader.RowIndex, values.Length);
                }
            }

            return statement;
        }

        private double CleanNumericData(string input)
        {
            double output = 0;
            if (Double.TryParse(input, out output))
            {
                return output;
            }

            return output;
        }


    }
}