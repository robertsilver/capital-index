using capital_index.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace capital_index_tests
{
    [TestClass]
    public class ExchangeRateApiTests
    {
        [TestMethod]
        public void Test_LoadExchangeRatesFromApi_Successfully()
        {
            IExchangeRate data = new ExchangeRatesFromApi("2019-10-01", "2019-10-31", "EUR");
            List<RateDetail> rates = new List<RateDetail>();

            Task.Run(async () =>
            {
                rates = await data.RetrieveFxRatesAsync();
            }).GetAwaiter().GetResult();

            Assert.IsNotNull(rates);
            Assert.AreEqual(rates.Count, 31);
        }

        [TestMethod]
        public void Test_RetrieveRatesFromApiForInvalidDate()
        {
            IExchangeRate data = new ExchangeRatesFromApi("2019-10-45", null, "EUR");
            List<RateDetail> rates = new List<RateDetail>();

            Task.Run(async () =>
            {
                rates = await data.RetrieveFxRatesAsync();
            }).GetAwaiter().GetResult();

            Assert.AreEqual(rates.Count, 0);
        }
    }
}
