using Currencies.Entities;
using Flurl;
using Flurl.Http;
using System;
using System.Threading.Tasks;
using System.Linq;
using Currencies.Common;

namespace Currencies
{
    public class BynCurrenciesApi : ICurrenciesApi
    {
        private const string BaseApiUrl = "https://www.nbrb.by/api/exrates";
        private readonly string CurrenciesApiUrl = $"{BaseApiUrl}/currencies";
        private readonly string CurrencyRateApiUrl = $"{BaseApiUrl}/rates/";
        private readonly string CurrencyRateDynamicsApiUrl = $"{BaseApiUrl}/rates/dynamics/";

        public async Task<CurrencyModel[]> GetCurrencies(DateTime? onDate = null)
        {
            var currencies = await GetCurrenciesInternal(onDate);
            if (onDate == null) onDate = DateTime.Today;
            return currencies.Where(x => x.DateEnd > onDate).Select(x => new CurrencyModel
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                CharCode = x.Abbreviation
            }).ToArray();
        }

        
        
        public async Task<CurrencyRateModel> GetCurrencyRate(string charCode, DateTime? onDate = null)
        {
            var currencyRate = await CallApi(() => CurrencyRateApiUrl
                    .AppendPathSegment(charCode)
                    .SetQueryParams(new
                    {
                        parammode = 2,
                        ondate = onDate?.ToString()
                    })
                    .GetJsonAsync<BynCurrencyRate>());
            return new CurrencyRateModel
            {
                Id = currencyRate.Id.ToString(),
                CharCode = charCode,
                Name = currencyRate.Name,
                Nominal = currencyRate.Scale,
                Rate = currencyRate.Rate,
                Date = onDate ?? DateTime.Today
            };
        }
      
        public async Task<CurrencyRateModel[]> GetDynamics(string charCode, DateTime start, DateTime end)
        {
            var currencies = await GetCurrenciesInternal(end);
            var currency = currencies.Where(x => x.DateEnd > end).Single(x => x.Abbreviation == charCode);

            var dynamics = await CallApi(() => CurrencyRateDynamicsApiUrl
                .AppendPathSegment(currency.Id)
                .SetQueryParams(new
                {
                    startdate = start.ToString(),
                    enddate = end.ToString()
                })
                .GetJsonAsync<BynCurrencyRateShort[]>());
            return dynamics.Select(RateShort => new CurrencyRateModel
            {
                Id = RateShort.Id.ToString(),
                CharCode = charCode,
                Name = currency.Name,
                Nominal = currency.Scale,
                Rate = RateShort.Rate,
                Date = RateShort.Date
            }).ToArray();
        }

        private Task<BynCurrency[]> GetCurrenciesInternal(DateTime? onDate = null)
        {
            return CallApi(() => CurrenciesApiUrl.GetJsonAsync<BynCurrency[]>());
        }

        private static async Task<T> CallApi<T>(Func<Task<T>> func)
        {
            try
            {
                return await func();
            }
            catch (FlurlHttpException e) when (e.StatusCode == 404)
            {

                throw new CurrencyNotAvailableException("Currency not available");
            }
        }


    }
}
