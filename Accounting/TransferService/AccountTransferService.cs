using Currencies.Common.Conversion;
using System;
using System.Threading.Tasks;

namespace Accounting
{
    public class AccountTransferService : IAccountTransferService
    {
        private readonly IAccountRepository _repository;
        private readonly IAccountAcquiringService _acquiringService;
        private readonly ICurrencyConversionService _conversionService;
        public event Action<Guid, Guid, decimal> Transfered;


        public AccountTransferService(
            IAccountRepository repository,
            IAccountAcquiringService acquiringService,
            ICurrencyConversionService conversionService)
        {
            _repository = repository;
            _acquiringService = acquiringService;
            _conversionService = conversionService;
        }

        public async Task Transfer(AccountTransferParameters transferParameters)
        {
            var fromAccount = await _repository.GetAccountById(transferParameters.From);
            var toAccount = await _repository.GetAccountById(transferParameters.To);

            if (fromAccount.CharCode == transferParameters.CurrencyCharCode)
            {
                await _acquiringService.Withdraw(transferParameters.From, transferParameters.Amount);

            }
            else
            {
                var withdraw = await _conversionService.ConvertFromTo(
                        transferParameters.CurrencyCharCode,
                        fromAccount.CharCode,
                        transferParameters.Amount);
                await _acquiringService.Withdraw(
                    transferParameters.From,
                    withdraw);
            }

            if (toAccount.CharCode == transferParameters.CurrencyCharCode)
            {
                await _acquiringService.Acquire(transferParameters.To, transferParameters.Amount);

            }
            else
            {
                var acquire = await _conversionService.ConvertFromTo(
                        transferParameters.CurrencyCharCode,
                        toAccount.CharCode,
                        transferParameters.Amount);
                await _acquiringService.Acquire(
                    transferParameters.To,
                    acquire);
            }
            Transfered(fromAccount.Id, toAccount.Id, transferParameters.Amount);
        }
    }
}
