using System;
using System.Threading.Tasks;

namespace Currencies
{
    public interface ICurrencyInfoService
    {
        Task<string[]> GetAvailableCurrencies();

        Task<double> GetCurrencyRate(string charCode, DateTime? onDate = null);

        Task<decimal> ConvertFromLocal(string charCode, decimal amount);

        Task<decimal> ConvertToLocal(string charCode, decimal amount);

        Task<double> GetMinRate(string charCode, DateTime start, DateTime end);

        Task<double> GetMaxRate(string charCode, DateTime start, DateTime end);

        Task<double> GetAverageRate(string charCode, DateTime start, DateTime end);
    }
}
