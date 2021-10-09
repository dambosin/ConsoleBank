using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Currencies.Common;

namespace Currencies
{
    public class CurrenciesApiCacheService : ICurrenciesApiCacheService
    {
        private readonly ICurrenciesApi _currenciesApi;
        private Dictionary<string, List<CurrencyRateModel>> _ratesCache = new();
        private readonly List<CurrencyModel> _currenciesCache = new();

        public Task Initialize()
        {
            return null;
        }

        public CurrenciesApiCacheService(ICurrenciesApi currenciesApi)
        {
            _currenciesApi = currenciesApi;
        }

        public async  Task<CurrencyModel[]> GetCurrencies(DateTime? onDate = null)
        {
            if (_currenciesCache.Any())
            {
                return _currenciesCache.ToArray();
            }
            var currencies = await _currenciesApi.GetCurrencies(onDate);
            _currenciesCache.AddRange(currencies);
            return currencies;
        }

        public async Task<CurrencyRateModel> GetCurrencyRate(string charCode, DateTime? onDate = null)
        {
            var key = GetKey(onDate);
            if (_ratesCache.ContainsKey(key))
            {
                var rate =_ratesCache[key].SingleOrDefault(rate => rate.CharCode == charCode);
                if (rate != null)
                {
                    return rate;
                }
            }
            
            return await GetNewCurrencyRate(charCode, onDate);
        }

        public Task<CurrencyRateModel[]> GetDynamics(string charCode, DateTime start, DateTime end)
        {
            return _currenciesApi.GetDynamics(charCode, start, end);
        }

        private async Task<CurrencyRateModel> GetNewCurrencyRate(string charCode, DateTime? onDate = null)
        {
            var newRate = await _currenciesApi.GetCurrencyRate(charCode, onDate);
            var key = GetKey(onDate);
            AddToCache(key, newRate);
            return newRate;
        }

        private string GetKey(DateTime? date = null)
        {
            return (date ?? DateTime.Today).ToString("d");
        }

        private void AddToCache( string date, CurrencyRateModel rate)
        {
            if (_ratesCache.ContainsKey(date))
            {
                var value = _ratesCache[date];
                value.Add(rate);
            }
            else
            {
                _ratesCache.Add(date, new List<CurrencyRateModel>{rate});
            }
        }
    }
}
