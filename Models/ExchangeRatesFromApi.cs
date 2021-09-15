using capital_index_utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace capital_index.Models
{
    public class ExchangeRatesFromApi : IExchangeRate
    {
        public ExchangeRatesFromApi(string from, string to = null, string baseCurrency = "EUR")
        {
            _configurationManager = new ConfigurationManager();

            _fixerApi = _configurationManager.GetValue<string>("FixerApi");
            _accessKey = _configurationManager.GetValue<string>("AccessKey");
            _baseCurrency = baseCurrency;

            DateTime tempFrom;
            if (DateTime.TryParse(from, out tempFrom))
                _fromDate = tempFrom;

            if (!string.IsNullOrEmpty(to))
            {
                var tempTo = DateTime.Parse(to);
                _toDate = tempTo;
            }
            else if (_fromDate.HasValue)
                _toDate = _fromDate.Value;
        }

        protected ConfigurationManager _configurationManager { get; }

        private string _fixerApi { get; set; }

        private string _accessKey { get; set; }

        private DateTime? _fromDate { get; set; }

        private DateTime _toDate { get; set; }

        private string _baseCurrency { get; set; }

        public async Task<List<RateDetail>> RetrieveFxRatesAsync()
        {
            List<RateDetail> rates = new List<RateDetail>();

            if (!_fromDate.HasValue)
                return rates;

            var count = 0;
            var url = GetFixerUrl(0);
            List<RateDetailDto> rateDto = new List<RateDetailDto>();

            while (_fromDate.Value.AddDays(count).Date <= _toDate.Date)
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var contentStream = await response.Content.ReadAsStreamAsync();

                    rateDto.Add(ParseData(contentStream));
                }

                count++;
                url = GetFixerUrl(count);
            }

            rates = ConvertDto(rateDto);

            return rates;
        }

        public List<Transaction> RetrieveTransactions(params string[] filenames)
        {
            throw new NotImplementedException();
        }

        private string GetFixerUrl(int daysToAdd)
        {
            var dateString = _fromDate.Value.AddDays(daysToAdd).ToString("yyyy-MM-dd");

            return $"{_fixerApi}{dateString}?access_key={_accessKey}&base={_baseCurrency}";
        }

        private RateDetailDto ParseData(Stream data)
        {
            RateDetailDto ratesDto = new RateDetailDto();

            var streamReader = new StreamReader(data);
            var jsonReader = new JsonTextReader(streamReader);

            JsonSerializer serializer = new JsonSerializer();

            try
            {
                ratesDto = serializer.Deserialize<RateDetailDto>(jsonReader);
            }
            catch (JsonReaderException ex)
            {
            }
            catch (Exception ex)
            {
            }

            return ratesDto;
        }

        private List<RateDetail> ConvertDto(List<RateDetailDto> dto)
        {
            List<RateDetail> rates = new List<RateDetail>();

            if (dto.FindAll(f => f.Rates == null).Count == dto.Count)
                return rates;

            foreach(var d in dto)
            {
                RateDetail temp = new RateDetail
                {
                    Date = d.Date,
                    Success = d.Success,
                    Rates = new List<Rates>()
                };

                foreach (var r in d.Rates)
                    temp.Rates.Add(new Rates
                    {
                        Currency = r.Key,
                        Rate = r.Value
                    });

                rates.Add(temp);
            }

            return rates;
        }

        public List<MergedData> MergeData()
        {
            throw new NotImplementedException();
        }
    }
}