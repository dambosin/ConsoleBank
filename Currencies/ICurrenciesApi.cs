using Currencies.Entities;
using System.Threading.Tasks;

namespace Currencies
{
    public interface ICurrenciesApi
    {
        public Task<Currency[]> GetCurrencies();
        public Task<CurrencyRate> GetCurrencyRate(int currencyId);
    }
}
