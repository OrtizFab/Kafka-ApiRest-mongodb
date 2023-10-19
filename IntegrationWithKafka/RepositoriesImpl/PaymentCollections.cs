using IntegrationWithKafka.DataAccess;
using IntegrationWithKafka.Models;
using IntegrationWithKafka.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace IntegrationWithKafka.RepositoriesImpl
{
    public class PaymentCollections: IPaymentRepository
    {
        private mongoDbConnection _MongoDBRepository = new mongoDbConnection();
        private IMongoCollection<Payments> Collection;

        public PaymentCollections()
        {
            Collection = _MongoDBRepository.Database.GetCollection<Payments>("payments");
        }

        public async Task DeleteTeam(string id)
        {
            var filter = Builders<Payments>.Filter.Eq(f => f.Id, new ObjectId(id));
            await Collection.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<Payments>> GetAllPayments()
        {
            return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Payments> getPaymentById(string id)
        {
            return await Collection.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();
        }

        public async Task InsertTeam(Payments payments)
        {
            await Collection.InsertOneAsync(payments);
        }

        public async Task UpdateTeam(Payments payments)
        {
            var filter = Builders<Payments>.Filter.Eq(f => f.Id, payments.Id);
            await Collection.ReplaceOneAsync(filter, payments);
        }
    }
}
