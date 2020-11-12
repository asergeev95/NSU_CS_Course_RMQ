using System;
using System.Threading.Tasks;
using ContractLibrary;
using MassTransit;

namespace Consumer
{
    public class PaymentReceivedConsumer : IConsumer<IPaymentReceivedContract>
    {
        public Task Consume(ConsumeContext<IPaymentReceivedContract> context)
        {
            var message = context.Message;
            Console.WriteLine($"Payment from {message.UserName} received at {message.DateTimeOffset}. Amount: {message.MoneyAmount}");
            return Task.CompletedTask;
        }
    }
}