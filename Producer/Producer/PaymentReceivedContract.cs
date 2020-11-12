using System;
using ContractLibrary;

namespace Producer
{
    public class PaymentReceivedContract : IPaymentReceivedContract
    {
        public DateTimeOffset DateTimeOffset { get; set; }
        public string UserName { get; set; }
        public double MoneyAmount { get; set; }
    }
}