using capital_index.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace capital_index_tests
{
    [TestClass]
    public class ExchangeRateMergeTests
    {
        [TestMethod]
        public void Test_MergedData()
        {
            var apiData = RetrieveApiData();
            var allTransactions = RetrieveDataFiles();

            IExchangeRate data = new ExchangeRatesMergeData(apiData, allTransactions);

            var mergedData = data.MergeData();

            Assert.IsNotNull(mergedData);
            Assert.AreEqual(mergedData.Count, 15000);
        }

        private List<RateDetail> RetrieveApiData()
        {
            IExchangeRate data = new ExchangeRatesFromApi("2019-10-01", "2019-10-31", "EUR");
            List<RateDetail> rates = new List<RateDetail>();

            Task.Run(async () =>
            {
                rates = await data.RetrieveFxRatesAsync();
            }).GetAwaiter().GetResult();

            return rates;
        }

        private List<Transaction> RetrieveDataFiles()
        {
            IExchangeRate data = new ExchangeRatesFromFile(@"C:\Projects\capital-index\DataToMerge\");

            return data.RetrieveTransactions(new string[] { "Data1.csv", "Data2.csv", "Data3.csv" });
        }
    }
}
