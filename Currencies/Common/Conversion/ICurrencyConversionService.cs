using System.Threading.Tasks;

namespace Currencies.Common.Conversion
{
    public interface ICurrencyConversionService
    {
        Task<decimal> ConvertFromLocal(string charCode, decimal amount);

        Task<decimal> ConvertToLocal(string charCode, decimal amount);

        Task<decimal> ConvertFromTo(string from, string to, decimal amount);
    }
}
