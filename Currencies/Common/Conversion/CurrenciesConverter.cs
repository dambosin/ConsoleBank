using Currencies.Common;
using System.Threading.Tasks;

namespace Currencies
{
    public static class CurrenciesConverter
    {
        public static decimal ConvertToLocal(CurrencyRateModel rate, decimal amount)
        {
            return amount / rate.Nominal * (decimal)rate.Rate;
        }

        public static decimal ConvertFromLocal(CurrencyRateModel rate, decimal amount)
        {
            return amount / (decimal)rate.Rate * rate.Nominal;
        }

        public static decimal ConvertFromTo(CurrencyRateModel from, CurrencyRateModel to, decimal amount)
        {
            return ConvertFromLocal(to, ConvertToLocal(from, amount));
        }
    }
}