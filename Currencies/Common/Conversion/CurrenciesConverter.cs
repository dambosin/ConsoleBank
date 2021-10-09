using Currencies.Common;

namespace Currencies
{
    public class CurrenciesConverter : ICurrenciesConverter
    {
        public double ConvertTo(CurrencyRateModel rate, int amount)
        {
            return amount / rate.Nominal * rate.Rate;
        }

        public double ConvertFrom(CurrencyRateModel rate, int amount)
        {
            return amount / rate.Rate * rate.Nominal;
        }
    }
}