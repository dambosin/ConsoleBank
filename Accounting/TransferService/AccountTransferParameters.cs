using System;

namespace Accounting
{
    public class AccountTransferParameters
    {
        public Guid From { get; set; }
        public Guid To { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCharCode { get; set; }
    }
}
