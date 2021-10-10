using System;
using System.Threading.Tasks;

namespace Accounting
{
    public class AccountAcquiringService : IAccountAcquiringService
    {
        private readonly IAccountRepository _repository;

        public  AccountAcquiringService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task Acquire(Guid id, decimal amount)
        {
            var account = await _repository.GetAccountById(id);
            account.Amount += amount;
        }

        public async Task Withdraw(Guid id, decimal amount)
        {
            var account = await _repository.GetAccountById(id);
            if(amount > account.Amount)
            {
                throw new InvalidOperationException("Not enought money");
            }
            account.Amount -= amount;
        }
    }
}
