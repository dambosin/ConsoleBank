using System;
using System.Threading.Tasks;

namespace Accounting
{
    public interface IAccountAcquiringService
    {
        Task Withdraw(Guid id, decimal amount);
        Task Acquire(Guid id, decimal amount);
    }
}
