using System.Linq;
using System.Threading.Tasks;

namespace Currencies
{
    public class CurrencyInfoService : ICurrencyInfoService
    {
        private readonly ICurrenciesApi _api;
        private readonly string[] _availableCurrencies = new[] {"USD", "RUB", "EUR"};
        public CurrencyInfoService(ICurrenciesApi currencyApi)
        {
            _api = currencyApi;
        }
        public async Task<string[]> GetAvailableCurrencies()
        {
            var currencies =  await _api.GetCurrencies();
            return currencies
                .Where(currency => _availableCurrencies.Contains(currency.Abbreviation))
                .Select(currency => currency.Name)
                .ToArray();
        }

        public async Task<double>  GetCurrencyRate(string abbreviation)
        {
            if (!_availableCurrencies.Contains(abbreviation))
            {
                return 0;
            }

            var currencyRate = await _api.GetCurrencyRate(abbreviation);
            return currencyRate.Rate;

        }
    }
}
