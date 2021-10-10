using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting
{
    public class AccountRepository : IAccountRepository
    {
        private readonly List<Account> _accounts = new();

        public Task AddAccount(Account account)
        {
            if (_accounts.All(x => x.Id != account.Id))
            {
                _accounts.Add(account);
            }

            return Task.CompletedTask;
            
        }

        public async Task DeleteAccount(Guid id)
        {
            var account = await GetAccountById(id);
            _accounts.Remove(account);
        }

        public Task<Account> GetAccountById(Guid id)
        {
            var account = _accounts.SingleOrDefault(x => x.Id == id);
            if (account == null)
            {
                throw new InvalidOperationException("Cannot find an account with ID:" + id);
            }
            return Task.FromResult(account);
        }
    }
}
