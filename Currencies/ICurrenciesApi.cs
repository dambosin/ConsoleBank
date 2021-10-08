using Currencies.Entities;
using System.Threading.Tasks;

namespace Currencies
{
    public interface ICurrenciesApi
    {
        public Task<Currency[]> GetCurrencies(bool afterDenomination = true);
        public Task<CurrencyRate> GetCurrencyRate(int currencyId);
        public Task<CurrencyRate> GetCurrencyRate(string currencyAbbreviation);
    }
}
