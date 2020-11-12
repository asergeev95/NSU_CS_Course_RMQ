using System;

namespace ContractLibrary
{
    public interface IPaymentReceivedContract
    {
        DateTimeOffset DateTimeOffset { get; set; }
        string UserName { get; set; }
        double MoneyAmount { get; set; }
    }
}