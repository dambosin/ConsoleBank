using Currencies.Entities;
using Flurl;
using Flurl.Http;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Currencies
{
    public class CurrenciesApi : ICurrenciesApi
    {
        private const string CurrenciesApiUrl = "https://www.nbrb.by/api/exrates/currencies";
        private const string CurrenciyRateApiUrl = "https://www.nbrb.by/api/exrates/rates/";

        public async Task<Currency[]> GetCurrencies(bool afterDenomination)
        {
            var currencies = await CallApi(() => CurrenciesApiUrl.GetJsonAsync<Currency[]>());
            return afterDenomination 
                ? currencies.Where(currency => currency.DateEnd > DateTime.Now).ToArray() 
                : currencies;
        }
        
        public Task<CurrencyRate> GetCurrencyRate(int currencyId)
        {
            return CallApi(() => CurrenciyRateApiUrl.AppendPathSegment(currencyId).GetJsonAsync<CurrencyRate>());
        }

        public Task<CurrencyRate> GetCurrencyRate(string currencyAbbreviation)
        {
            return CallApi(() => CurrenciyRateApiUrl
                .AppendPathSegment(currencyAbbreviation)
                .SetQueryParam("parammode", 2)
                .GetJsonAsync<CurrencyRate>());
        }

        private async Task<T> CallApi<T>(Func<Task<T>> func)
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
