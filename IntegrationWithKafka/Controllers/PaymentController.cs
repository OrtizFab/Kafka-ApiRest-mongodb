using Confluent.Kafka;
using IntegrationWithKafka.Models;
using IntegrationWithKafka.Repositories;
using IntegrationWithKafka.RepositoriesImpl;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IntegrationWithKafka.Controllers
{
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository _payment = new PaymentCollections();
        private readonly ProducerConfig config;
        public PaymentController(ProducerConfig config)
        {
            this.config = config;

        }
        // POST api/values
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] Payments value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _payment.InsertTeam(value);
           
            //Serialize 
            string serializedOrder = JsonConvert.SerializeObject(value);

            Console.WriteLine("========");
            Console.WriteLine("Info: PaymentController => Post => Recieved a new payment:");
            Console.WriteLine(serializedOrder);
            Console.WriteLine("=========");

            var producer = new ProducerWrapper(this.config, "payment");
            await producer.writeMessage(serializedOrder);

            return Created("TransactionId", "Your payment is in progress");
        }
    }
}
