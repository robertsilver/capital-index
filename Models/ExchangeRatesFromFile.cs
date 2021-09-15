using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace capital_index.Models
{
    public class ExchangeRatesFromFile : IExchangeRate
    {
        public ExchangeRatesFromFile(string fullpath)
        {
            _path = fullpath;
        }

        private string _path { get; set; }

        public List<MergedData> MergeData()
        {
            throw new NotImplementedException();
        }

        public Task<List<RateDetail>> RetrieveFxRatesAsync()
        {
            throw new NotImplementedException();
        }

        public List<Transaction> RetrieveTransactions(params string[] filenames)
        {
            List<Transaction> transactions = new List<Transaction>();

            foreach (var f in filenames)
            {
                var fullpath = Path.Combine(_path, f);

                if (!File.Exists(fullpath))
                    continue;

                var csvRows = File.ReadAllLines(fullpath).ToList();

                foreach (var row in csvRows.Skip(1))
                {
                    var columns = row.Split(',');

                    Transaction oneTrans = new Transaction
                    {
                        Amount = Convert.ToDouble(columns[3]),
                        Country = columns[1],
                        Date = DateTime.Parse(columns[0]),
                        Currency = columns[2]
                    };

                    transactions.Add(oneTrans);
                }
            }

            return transactions;
        }
    }
}