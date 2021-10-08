using Currencies.Entities;
using Flurl;
using Flurl.Http;
using System;
using System.Threading.Tasks;

namespace Currencies
{
    public partial class CurrenciesApi : ICurrenciesApi
    {
        private const string CurrenciesApiUrl = "https://www.nbrb.by/api/exrates/currencies";
        private const string CurrenciyRateApiUrl = "https://www.nbrb.by/api/exrates/rates/";

        public Task<Currency[]> GetCurrencies()
        {
            return CallApi(() =>
            {
                return CurrenciesApiUrl.GetJsonAsync<Currency[]>();
            });
        }
        
        public Task<CurrencyRate> GetCurrencyRate(int currencyId)
        {
            return CallApi(() =>
            {
                return CurrenciyRateApiUrl.AppendPathSegment(currencyId).GetJsonAsync<CurrencyRate>();
            });
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
