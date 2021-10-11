using System;
using System.Threading.Tasks;

namespace Accounting
{
    public interface IAccountAcquiringService
    { 
        event Action<Guid, decimal> Acquired;
        event Action<Guid, decimal> Withdrawn;
        Task Withdraw(Guid id, decimal amount);
        Task Acquire(Guid id, decimal amount);
    }
}
