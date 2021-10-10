using System;
using System.Threading.Tasks;

namespace Accounting
{
    public interface IAccountRepository
    {
        Task AddAccount(Account account);
        Task DeleteAccount(Guid id);
        Task<Account> GetAccountById(Guid id);
    }
}
