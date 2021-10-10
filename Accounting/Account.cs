using System;

namespace Accounting
{
    public class Account
    {
        public Guid Id { get; set; }
        public string CharCode { get; set; }
        public decimal Amount { get; set; }
        public Guid UserId { get; set; }
    }
}
