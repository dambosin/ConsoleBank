using System;

namespace Accounting.TrackingService
{
    public class AccountOperationInfo
    {
        public AccountOperationType OperationType { get; set; }
        public decimal Amount { get; set; }
        public Guid AccountId { get; set; }
    }
}
