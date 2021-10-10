using System.Threading.Tasks;

namespace Currencies.Common.Conversion
{
    public class CurrencyConversionService : ICurrencyConversionService
    {
        private readonly ICurrenciesApiCacheService _api;
        //  amount
        // amount
        public CurrencyConversionService(ICurrenciesApiCacheService currencyApi)
        {
            _api = currencyApi;
        }

        public async Task<decimal> ConvertFromLocal(string charCode, decimal amount)
        {
            var rate = await GetCurrencyRateInternal(charCode);
            return rate != null ? CurrenciesConverter.ConvertFromLocal(rate, amount) : 0;
        }

        public async  Task<decimal> ConvertToLocal(string charCode, decimal amount)
        {
            var rate = await GetCurrencyRateInternal(charCode);
            return rate != null ? CurrenciesConverter.ConvertToLocal(rate, amount) : 0;
        }

        public async Task<decimal> ConvertFromTo(string from, string to, decimal amount)
        {
            var fromRate = await GetCurrencyRateInternal(from);
            var toRate = await GetCurrencyRateInternal(to);
            return CurrenciesConverter.ConvertFromTo(fromRate, toRate, amount);
        }

        private async Task<CurrencyRateModel> GetCurrencyRateInternal(string charCode)
        {
            return await _api.GetCurrencyRate(charCode);
        }

    }
}
