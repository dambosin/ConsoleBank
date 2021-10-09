using System;
using System.Threading.Tasks;

namespace Currencies
{
    public interface ICurrencyInfoService
    {
         Task<string[]> GetAvailableCurrencies();

        Task<double> GetCurrencyRate(string charCode, DateTime? onDate = null);

        Task<double> ConvertFrom(string charCode, int amount);

        Task<double> ConvertTo(string charCode, int amount);

        Task<double> GetMinRate(string charCode, DateTime start, DateTime end);

        Task<double> GetMaxRate(string charCode, DateTime start, DateTime end);

        Task<double> GetAverageRate(string charCode, DateTime start, DateTime end);
    }
}
