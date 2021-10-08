using System;
using System.Threading.Tasks;
using Currencies;
using System.Linq;

namespace ConsoleBank
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var api = new CurrenciesApi();
            Currency[] currencies = await api.GetCurrencies();
            foreach(var currency in currencies)
            {
                Console.WriteLine(currency);
            }

            var currencyRate = await api.GetCurrencyRate(20000);
            Console.WriteLine(currencyRate);





            Console.ReadKey();
        }
    }
}
