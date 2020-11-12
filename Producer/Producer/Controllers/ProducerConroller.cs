using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Producer.Controllers
{
    [Route("api/producer")]
    [ApiController]
    public class ProducerConroller: ControllerBase
    {
        private readonly PaymentReceivedProducer _paymentReceivedProducer;

        public ProducerConroller(PaymentReceivedProducer paymentReceivedProducer)
        {
            _paymentReceivedProducer = paymentReceivedProducer;
        }
        [HttpPost]
        public async Task SendEvent(double money)
        {
            await _paymentReceivedProducer.SentPaymentReceivedEvent(money);
        }
    }
}