using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace IntegrationWithKafka.Models
{
    public class Payments
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public int PaymentId { get; set; }
        public string Client { get; set; } = string.Empty;
        public string PaymentType { get; set; } = string.Empty;
        public double PaymentValue { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus OrderStatus { get; set; }

    }

    public enum PaymentStatus
    {
        IN_PROGRESS,
        COMPLETED,
        REJECTED
    }
}
