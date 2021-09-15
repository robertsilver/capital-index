using capital_index.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace capital_index_tests
{
    [TestClass]
    public class ExchangeRateFileTests
    {
        public ExchangeRateFileTests()
        {
        }

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        public void Test_LoadExchangeRatesFromNonExistentFile()
        {
            IExchangeRate data = new ExchangeRatesFromFile(@"C:\Projects\capital-index\DataToMerge\");
            List<Transaction> allTransactions = new List<Transaction>();

            allTransactions = data.RetrieveTransactions(new string[] { "dummy.csv" });
            Assert.IsNotNull(allTransactions);
            Assert.AreEqual(allTransactions.Count, 0);
        }

        [TestMethod]
        public void Test_LoadExchangeRatesFromFile_SomeFilesWillExist_OthersWillNot()
        {
            IExchangeRate data = new ExchangeRatesFromFile(@"C:\Projects\capital-index\DataToMerge\");
            List<Transaction> allTransactions = new List<Transaction>();

            allTransactions = data.RetrieveTransactions(new string[] { "dummy.csv", "Data1.csv", "test.docx", "Data2.csv" });
            Assert.IsNotNull(allTransactions);
            Assert.AreEqual(allTransactions.Count, 10000);
        }

        [TestMethod]
        public void Test_LoadExchangeRatesFromOneFile_Successfully()
        {
            IExchangeRate data = new ExchangeRatesFromFile(@"C:\Projects\capital-index\DataToMerge\");
            List<Transaction> allTransactions = new List<Transaction>();

            allTransactions = data.RetrieveTransactions(new string[] { "Data1.csv"});
            Assert.IsNotNull(allTransactions);
            Assert.AreEqual(allTransactions.Count, 5000);
        }

        [TestMethod]
        public void Test_LoadExchangeRatesFromThreeFiles_Successfully()
        {
            IExchangeRate data = new ExchangeRatesFromFile(@"C:\Projects\capital-index\DataToMerge\");
            List<Transaction> allTransactions = new List<Transaction>();

            allTransactions = data.RetrieveTransactions(new string[] { "Data1.csv", "Data2.csv", "Data3.csv" });
            Assert.IsNotNull(allTransactions);
            Assert.AreEqual(allTransactions.Count, 15000);
        }
    }
}
