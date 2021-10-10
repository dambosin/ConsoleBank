using System;
using System.Threading.Tasks;

namespace Accounting
{
    public class AccountManagementService : IAccountManagmentService
    {
        private readonly IAccountRepository _repository;
        private readonly IAccountAcquiringService _acquiringService;
        private readonly IAccountTransferService _transferService;

        public AccountManagementService(
            IAccountRepository repository, 
            IAccountAcquiringService acquiringService, 
            IAccountTransferService transferService)
        {
            _repository = repository;
            _acquiringService = acquiringService;
            _transferService = transferService;
        }

        public Task Acquire(Guid accountId, decimal amount)
        {
            AssertValidAmount(amount);

            return _acquiringService.Acquire(accountId, amount);
        }

        public Task<Guid> CreateAccount(string currencyCharCode, Guid userId)
        {
            var id = Guid.NewGuid();
            _repository.AddAccount(new Account
            {
                Id = id,
                CharCode = currencyCharCode,
                Amount = 0,
                UserId = userId
            });
            return Task.FromResult(id);
        }

        public Task DeleteAccount(Guid accountId)
        {
            return _repository.DeleteAccount(accountId);
        }

        public Task<Account> GetAccountById(Guid accountId)
        {
            return  _repository.GetAccountById(accountId);
        }

        public Task Transfer(Guid fromAccountId, Guid toAccountId, decimal amount, string currencyCharCode)
        {
            AssertValidAmount(amount);

            return _transferService.Transfer(new AccountTransferParameters
                {
                    From = fromAccountId,
                    To = toAccountId,
                    Amount = amount,
                    CurrencyCharCode = currencyCharCode
                });
        }

        public Task Withdraw(Guid accountId, decimal amount)
        {
            AssertValidAmount(amount);
            return _acquiringService.Withdraw(accountId, amount);
        }
        public void AssertValidAmount(decimal amount)
        {
            if( amount <= 0)
            {
                throw new InvalidOperationException("Invalid amount value:" + amount);
            }
        }
    }
}
