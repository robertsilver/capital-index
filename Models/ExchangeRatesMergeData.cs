using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace capital_index.Models
{
    public class ExchangeRatesMergeData : IExchangeRate
    {
        public ExchangeRatesMergeData(List<RateDetail> rates, List<Transaction> allTransactions)
        {
            _rates = rates;
            _allTransactions = allTransactions;
        }

        private List<RateDetail> _rates;

        private List<Transaction> _allTransactions;

        public Task<List<RateDetail>> RetrieveFxRatesAsync()
        {
            throw new NotImplementedException();
        }

        public List<Transaction> RetrieveTransactions(params string[] filenames)
        {
            throw new NotImplementedException();
        }

        public List<MergedData> MergeData()
        {
            List<MergedData> merged = new List<MergedData>();

            foreach (var a in _allTransactions)
            {
                var findByDate = _rates.Find(f => f.Date == a.Date);
                if (findByDate != null)
                {
                    var findCurrency = findByDate.Rates.Find(f => f.Currency == a.Currency);
                    if (findCurrency != null)
                        merged.Add(new MergedData
                        {
                            Amount = a.Amount,
                            AmountEur = a.Amount / findCurrency.Rate,
                            Country = a.Country,
                            Currency = a.Currency,
                            Date = a.Date,
                            ExchangeRate = findCurrency.Rate                            
                        });
                }
            }

            return merged;
        }
    }
}