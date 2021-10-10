using System;
using System.Threading.Tasks;

namespace Accounting
{
    public interface IAccountManagmentService
    {
        Task<Guid> CreateAccount(string currencyCharCode, Guid userId);

        Task DeleteAccount(Guid accountId);

        Task Withdraw(Guid accountId, decimal amount);
        
        Task Acquire(Guid accountId, decimal amount);

        Task Transfer(Guid fromAccountId, Guid toAccountId, decimal amount, string currencyCharCode);

        Task<Account> GetAccountById(Guid accountId);
    }
}
