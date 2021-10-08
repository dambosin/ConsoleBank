using System;
using System.Threading.Tasks;
using Currencies;

namespace ConsoleBank
{
    class Program
    {
        private static ICurrencyInfoService _infoService = new CurrencyInfoService(new CurrenciesApi());
        static async Task Main(string[] args)
        {
            
            
            var currencies = await _infoService.GetAvailableCurrencies();
            foreach(var currency in currencies)
            {
                Console.WriteLine(currency);
            }
            var Rate = await _infoService.GetCurrencyRate("UAH");
            Console.WriteLine(Rate);





            Console.ReadKey();
        }
    }
}
