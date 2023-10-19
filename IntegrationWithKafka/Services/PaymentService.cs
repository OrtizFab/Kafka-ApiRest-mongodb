using Confluent.Kafka;
using IntegrationWithKafka.Models;
using Newtonsoft.Json;

namespace IntegrationWithKafka.Services
{
    public class PaymentService: BackgroundService
    {
        private readonly ConsumerConfig consumerConfig;
        private readonly ProducerConfig producerConfig;
        public PaymentService(ConsumerConfig consumerConfig, ProducerConfig producerConfig)
        {
            this.consumerConfig = consumerConfig;
            this.producerConfig = producerConfig;        
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("PaymentProcessing Service Started");

            while (!stoppingToken.IsCancellationRequested)
            {
                var consumerHelper = new ConsumerWrapper(consumerConfig, "payment");
                string paymentRequest = consumerHelper.readMessage();

                //Deserilaize 
                Payments? payments = JsonConvert.DeserializeObject<Payments>(paymentRequest);

                //TODO:: Process Order
                Console.WriteLine($"Info: ProyectHandler => Processing the order for Mr./Mrs : {payments?.Client}");
                payments.OrderStatus = PaymentStatus.COMPLETED;

                //Write to ReadyToShip Queue

                var producerWrapper = new ProducerWrapper(producerConfig, "readytoship");
                await producerWrapper.writeMessage(JsonConvert.SerializeObject(payments));
            }
        }
    }
}
