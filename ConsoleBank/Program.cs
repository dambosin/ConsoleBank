using System;
using System.Threading.Tasks;
using Accounting;
using Accounting.TrackingService;
using Currencies;
using Currencies.Apis.Rub;
using Currencies.Common.Conversion;

namespace ConsoleBank
{
    class Program
    {
        private static ICurrenciesApiCacheService _apiCache = 
            new CurrenciesApiCacheService(new BynCurrenciesApi());
        
        private static ICurrencyConversionService _conversionService = 
            new CurrencyConversionService(_apiCache);

        private static ICurrencyInfoService _infoService = 
            new CurrencyInfoService(_apiCache, _conversionService);

        private static IAccountRepository _repository = 
            new AccountRepository();

        private static IAccountAcquiringService _acquiringService = 
            new AccountAcquiringService(_repository);

        private static IAccountTransferService _transferService = 
            new AccountTransferService(
                _repository,
                _acquiringService,
                _conversionService);

        private static IAccountManagmentService _accountManagmentService =
            new AccountManagementService(
                _repository,
                _acquiringService,
                _transferService);

        private static IAccountOperationTrackingService _trackingService = 
            new AccountOperationTrackingService(
                _acquiringService, 
                _transferService);

        static async Task Main(string[] args)
        {
            var userId = Guid.NewGuid();

            var accountId1 = await _accountManagmentService.CreateAccount("BYN", userId);
            var accountId2 = await _accountManagmentService.CreateAccount("USD", userId);

            await _accountManagmentService.Acquire(accountId1, 1000);
            await _accountManagmentService.Acquire(accountId2, 300);

            var account1 = await _accountManagmentService.GetAccountById(accountId1);
            var account2 = await _accountManagmentService.GetAccountById(accountId2);

            Console.WriteLine($"{account1.Id} - {account1.CharCode} - {account1.Amount} - {account1.UserId}");
            Console.WriteLine($"{account2.Id} - {account2.CharCode} - {account2.Amount} - {account2.UserId}");

            await _accountManagmentService.Withdraw(accountId1, 200);

            account1 = await _accountManagmentService.GetAccountById(accountId1);
            Console.WriteLine($"{account1.Id} - {account1.CharCode} - {account1.Amount} - {account1.UserId}");

            await _accountManagmentService.Transfer(accountId1, accountId2, 100, "EUR");

            account1 = await _accountManagmentService.GetAccountById(accountId1);
            account2 = await _accountManagmentService.GetAccountById(accountId2);

            Console.WriteLine($"{account1.Id} - {account1.CharCode} - {account1.Amount} - {account1.UserId}");
            Console.WriteLine($"{account2.Id} - {account2.CharCode} - {account2.Amount} - {account2.UserId}");

            await _accountManagmentService.DeleteAccount(accountId2);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            var result = _trackingService.GetOperations();

            foreach (var item in result)
            {
                Console.WriteLine($"{item.AccountId} - {item.Amount} - {item.OperationType}");
            }
            //account2 = await _accountManagmentService.GetAccountById(accountId2);



            //var currencies = await _infoService.GetAvailableCurrencies();
            //foreach(var currency in currencies)
            //{
            //    Console.WriteLine(currency);
            //}

            //var Usd = await _infoService.GetCurrencyRate("USD");
            //var Byn = await _infoService.GetCurrencyRate("BYN"); 
            //var Eur = await _infoService.GetCurrencyRate("EUR"); 
            //var Rub = await _infoService.GetCurrencyRate("RUB"); 

            //Console.WriteLine($"USD rate: {Usd}");
            //Console.WriteLine($"BYN rate: {Byn}");
            //Console.WriteLine($"EUR rate: {Eur}");
            //Console.WriteLine($"RUB rate: {Rub}");

            //var convert1 = await _infoService.ConvertFromLocal("USD", 1000);
            //var convert2 = await _infoService.ConvertToLocal("BYN", 10000);
            //var convert3 = await _infoService.ConvertFromLocal("EUR", 1000);
            //var convert4 = await _infoService.ConvertToLocal("RUB", 1000);

            //Console.WriteLine($"convert1 : {convert1}");
            //Console.WriteLine($"convert2 : {convert2}");
            //Console.WriteLine($"convert3 : {convert3}");
            //Console.WriteLine($"convert4 : {convert4}");

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
