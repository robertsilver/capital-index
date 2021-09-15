using capital_index.Models.Enums;
using System;

namespace capital_index.Models
{
    public class Transaction
    {
        public DateTime Date { get; set; }

        public string Country { get; set; }

        public string Currency { get; set; }

        public double Amount { get; set; }
    }

    public class GroupedData
    {
        public Countries Country { get; set; }

        public double TotalAmountEur { get; set; }
    }
}