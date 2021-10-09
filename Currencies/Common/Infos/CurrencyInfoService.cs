using Currencies.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Currencies
{
    public class CurrencyInfoService : ICurrencyInfoService
    {

        private readonly ICurrenciesConverter _converter;
        private readonly ICurrenciesApiCacheService _api;
        private readonly string[] _availableCurrencies = new[] {"USD", "RUB", "EUR", "BYN"};
        
        public CurrencyInfoService(ICurrenciesApiCacheService currencyApi, ICurrenciesConverter converter)
        {
            _api = currencyApi;
            _converter = converter;
        }
        
        public async Task<string[]> GetAvailableCurrencies()
        {
            var currencies =  await _api.GetCurrencies();
            return currencies
                .Where(currency => _availableCurrencies.Contains(currency.CharCode))
                .Select(currency => currency.Name)
                .ToArray();
        }

        public async Task<double> GetCurrencyRate(string charCode, DateTime? ondate = null)
        {
            return (await GetCurrencyRateInternal(charCode, ondate))?.Rate ?? 0d;
        }

        public async Task<double> ConvertTo(string charCode, int amount)
        {
            var rate = await GetCurrencyRateInternal(charCode);
            return rate != null ? _converter.ConvertTo(rate, amount) : 0;
        }

        public async Task<double> ConvertFrom(string charCode, int amount)
        {
            var rate = await GetCurrencyRateInternal(charCode);
            return rate != null ? _converter.ConvertFrom(rate, amount) : 0;
        }

        public async Task<double> GetMinRate(string charCode, DateTime start, DateTime end)
        {
            var rates = await _api.GetDynamics(charCode, start, end);

            return rates.Min(rate => rate.Rate);
        }

        public async Task<double> GetMaxRate(string charCode, DateTime start, DateTime end)
        {
            var rates = await GetDynamics(charCode, start, end);

            return rates.Max(rate => rate.Rate);
        }

        public async Task<double> GetAverageRate(string charCode, DateTime start, DateTime end)
        {
            var rates = await GetDynamics(charCode, start, end);

            return rates.Average(rate => rate.Rate);
        }

        private async Task<CurrencyRateModel[]> GetDynamics(string charCode, DateTime start, DateTime end)
        {
            return await _api.GetDynamics(charCode, start, end);
        }

        private async Task<CurrencyRateModel> GetCurrencyRateInternal(string charCode, DateTime? onDate = null)
        {
            return _availableCurrencies.Contains(charCode)
                ? await _api.GetCurrencyRate(charCode, onDate)
                : null;
        }

        
    }
}
