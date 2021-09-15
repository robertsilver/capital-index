using capital_index.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace capital_index.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var ratesFromApi = RetrieveRatesFromApi();
            var allTransactions = RetrieveTransactions();
            var mergedData = MergeTheData(ratesFromApi, allTransactions);
            var groupedData = GroupByCountry(mergedData);

            return View(groupedData);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private List<RateDetail> RetrieveRatesFromApi()
        {
            IExchangeRate apiRates = new ExchangeRatesFromApi("2019-10-01", "2019-10-31");

            List<RateDetail> rates = new List<RateDetail>();

            Task.Run(async () =>
            {
                rates = await apiRates.RetrieveFxRatesAsync();
            }).GetAwaiter().GetResult();

            return rates;
        }

        private List<Transaction> RetrieveTransactions()
        {
            IExchangeRate data = new ExchangeRatesFromFile(@"C:\Projects\capital-index\DataToMerge\");

            return data.RetrieveTransactions(new string[] { "Data1.csv", "Data2.csv", "Data3.csv" });
        }

        private List<MergedData> MergeTheData(List<RateDetail> ratesFromApi, List<Transaction> allTrans)
        {
            IExchangeRate data = new ExchangeRatesMergeData(ratesFromApi, allTrans);
            return data.MergeData();
        }

        private List<GroupedData> GroupByCountry(List<MergedData> data)
        {
            List<GroupedData> groupedData = new List<GroupedData>();

            var euTotal = data.Where(w => w.Country == "Austria" || w.Country == "Italy" || w.Country == "Belgium" || w.Country == "Latvia").Sum(s => s.AmountEur);
            groupedData.Add(new GroupedData
            {
                Country = Models.Enums.Countries.EU,
                TotalAmountEur = euTotal
            });

            var rowTotal = data.Where(w => w.Country == "Chile" || w.Country == "Qatar" || w.Country == "United Arab Emirates" || w.Country == "United States of America").Sum(s => s.AmountEur);
            groupedData.Add(new GroupedData
            {
                Country = Models.Enums.Countries.ROW,
                TotalAmountEur = rowTotal
            });

            var ukTotal = data.Where(w => w.Country == "United Kingdom").Sum(s => s.AmountEur);
            groupedData.Add(new GroupedData
            {
                Country = Models.Enums.Countries.UnitedKingdom,
                TotalAmountEur = ukTotal
            });

            var ausTotal = data.Where(w => w.Country == "Australia").Sum(s => s.AmountEur);
            groupedData.Add(new GroupedData
            {
                Country = Models.Enums.Countries.Australia,
                TotalAmountEur = ausTotal
            });

            var saTotal = data.Where(w => w.Country == "South Africa").Sum(s => s.AmountEur);
            groupedData.Add(new GroupedData
            {
                Country = Models.Enums.Countries.SouthAfrica,
                TotalAmountEur = saTotal
            });

            return groupedData.OrderByDescending(o => o.TotalAmountEur).ToList();
        }
    }
}