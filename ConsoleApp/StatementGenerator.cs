using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using StatemeentProcessor;
using StatemeentProcessor.Classes;
using CsvHelper;

namespace ConsoleApp {
    internal class StatementGenerator {
        public void StartProcessing() {
            string directoryName = @"/Users/shankar/Documents/Personal Documents/Banking/Statements";
            var files = System.IO.Directory.GetFiles(directoryName,"*.csv");

            List<StatementDetails> StatementResults = new List<StatementDetails>();

            foreach(var file in files){
                var statement = ParseCSV(file);
                var temp = statement.ToList();
                Processor processor = new Processor();
                StatementResults = processor.ProcessStatement(statement);
            }

            foreach(var s in StatementResults) {
                Console.WriteLine(string.Format("To: {0}, Desc: {1}, Amt: {2}", s.To, s.Desc, s.Amount));
            }
        }

        private IEnumerable<StatementRow> ParseCSV(string file) {
            using (var reader = new StreamReader(file)) {
                using (var csv = new CsvReader(reader)) {
                    csv.Configuration.HasHeaderRecord = true;

                    var records = csv.GetRecords<StatementRow>();
                    return records;
                }
            }
        }
    }
}