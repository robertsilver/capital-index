using System;

namespace capital_index.Models
{
    public class MergedData
    {
        public DateTime Date { get; set; }

        public string Country { get; set; }

        public string Currency { get; set; }

        public double Amount { get; set; }

        public double ExchangeRate { get; set; }

        public double AmountEur { get; set; }
    }
}