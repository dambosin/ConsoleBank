using System.Threading.Tasks;

namespace Currencies
{
    public interface ICurrencyInfoService
    {
        public Task<string[]> GetAvailableCurrencies();

        public Task<double> GetCurrencyRate(string abbreviation);
    }
}
