using Currencies.Common;

namespace Currencies
{
    public interface ICurrenciesConverter
    {
        double ConvertTo(CurrencyRateModel rate, int amount);
        double ConvertFrom(CurrencyRateModel rate, int amount);
    }
}