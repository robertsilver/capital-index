using System.Collections.Generic;
using System.Threading.Tasks;

namespace capital_index.Models
{
    public interface IExchangeRate
    {
        Task<List<RateDetail>> RetrieveFxRatesAsync();

        List<Transaction> RetrieveTransactions(params string[] filenames);

        List<MergedData> MergeData();
    }
}