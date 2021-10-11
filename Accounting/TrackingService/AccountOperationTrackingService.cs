using System;
using System.Collections.Generic;

namespace Accounting.TrackingService
{
    public class AccountOperationTrackingService : IAccountOperationTrackingService
    {
        List<AccountOperationInfo> _operations = new();

        public AccountOperationTrackingService(IAccountAcquiringService acquiringService, IAccountTransferService transferService)
        {
            acquiringService.Acquired += AcquiringEventHandler;
            acquiringService.Withdrawn += WithdrawingEventHandler;
            transferService.Transfered += TransferEventHandler;
        }

        private void TransferEventHandler(Guid from, Guid to, decimal amount)
        {
            AddOperation(from, -amount, AccountOperationType.Transfer);

            AddOperation(to, amount, AccountOperationType.Transfer);
        }

        public void AcquiringEventHandler(Guid accountId, decimal amount)
        {
            AddOperation(accountId, amount, AccountOperationType.Acquire);
        }

        public void WithdrawingEventHandler(Guid accountId, decimal amount)
        {
            AddOperation(accountId, amount, AccountOperationType.Withdraw);
        }

        public void AddOperation(Guid id, decimal amount, AccountOperationType type)
        {
            _operations.Add(new AccountOperationInfo
            {
                OperationType = type,
                AccountId = id,
                Amount = amount
            });
        }

        public List<AccountOperationInfo> GetOperations()
        {
            return _operations;
        }
    }
}
