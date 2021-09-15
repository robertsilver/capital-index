using System;
using System.Collections.Generic;

namespace capital_index.Models
{
    public class RateDetailDto
    {
        public bool Success { get; set; }

        public DateTime Date { get; set; }

        public string Quote { get; set; }

        public Dictionary<string, double> Rates { get; set; }
    }

    public class RateDetail
    {
        public bool Success { get; set; }
        public DateTime Date { get; set; }
        public List<Rates> Rates { get; set; }
    }

    public class Rates
    {
        public string Currency { get; set; }

        public double Rate { get; set; }
    }
}