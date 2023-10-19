using MongoDB.Driver;

namespace IntegrationWithKafka.DataAccess
{
    public class mongoDbConnection
    {
        public MongoClient Client;
        public IMongoDatabase Database;

        public mongoDbConnection()
        {
            Client = new MongoClient("mongodb://localhost:27017");
            Database = Client.GetDatabase("unisys");
        }
    }

}
   
