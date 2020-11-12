using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace Producer
{
    public class PaymentReceivedProducer
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IConfiguration _configuration;

        public PaymentReceivedProducer(ISendEndpointProvider sendEndpointProvider, IConfiguration configuration)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _configuration = configuration;
        }

        public async Task SentPaymentReceivedEvent(double moneyAmount)
        {
            var message = new PaymentReceivedContract
            {
                MoneyAmount = moneyAmount, UserName = "v.pupkin", DateTimeOffset = DateTimeOffset.Now
            };
            
            var hostConfig = new MassTransitConfiguration();
            _configuration.GetSection("MassTransit").Bind(hostConfig);
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(hostConfig.GetQueueAddress("test-queue-name"));
            await endpoint.Send(message);
        }
        
    }
}