using System;
using System.Threading.Tasks;

namespace Accounting
{
    public interface IAccountTransferService
    {
        event Action<Guid, Guid, decimal> Transfered;
        Task Transfer(AccountTransferParameters transferParameters);
    }
}
