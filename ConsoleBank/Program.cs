using System;
using System.Threading.Tasks;
using Currencies;
using Currencies.Apis.Rub;

namespace ConsoleBank
{
    class Program
    {
        private static ICurrencyInfoService _infoService = new CurrencyInfoService(new CurrenciesApiCacheService(new RubCurrenciesApi()), new CurrenciesConverter());
        static async Task Main(string[] args)
        {
            
            
            var currencies = await _infoService.GetAvailableCurrencies();
            foreach(var currency in currencies)
            {
                Console.WriteLine(currency);
            }

            var Usd = await _infoService.GetCurrencyRate("USD");
            var Byn = await _infoService.GetCurrencyRate("BYN"); 
            var Eur = await _infoService.GetCurrencyRate("EUR"); 

            Console.WriteLine($"USD rate: {Usd}");
            Console.WriteLine($"BYN rate: {Byn}");
            Console.WriteLine($"EUR rate: {Eur}");

            //var convert = await _infoService.ConvertFrom("USD", 1000);
            //var convert2 = await _infoService.ConvertTo("RUB", 10000);
            //var convert3 = await _infoService.ConvertFrom("USD", 1000);

            //Console.WriteLine($"1000 BYN in USD : {convert}");
            //Console.WriteLine($"10000 RUB in BYN : {convert2}");
            //Console.WriteLine($"1000 BYN in USD : {convert3}");

            //var average = await _infoService.GetAverageRate("EUR", DateTime.Today.AddYears(-1), DateTime.Today);
            //var min = await _infoService.GetMinRate("EUR", DateTime.Today.AddYears(-1), DateTime.Today);
            //var max = await _infoService.GetMaxRate("EUR", DateTime.Today.AddYears(-1), DateTime.Today);

            //Console.WriteLine($"Average : {average}");
            //Console.WriteLine($"Min : {min}");
            //Console.WriteLine($"Max : {max}");






            Console.ReadKey();
        }
    }
}
