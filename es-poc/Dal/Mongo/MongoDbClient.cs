using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace es_poc.Dal.Mongo
{
    public class MongoDbClient
{
        private IMongoDatabase m_Database;
        public MongoDbClient() {
            var client = new MongoClient("mongodb://localhost");
            m_Database = client.GetDatabase("ES-POC");

            // Insert value into database
            var collection = m_Database.GetCollection<Data>("movies");
            collection.InsertOne(new Data()
            {
                Id = "Move 1",
                Movie = "The Dark Knight Rises"
            });
        }

        private class Data
        {
            [BsonId]
            public string Id { get; set; }
            [BsonElement("name")]
            public string Movie { get; set; }
        }
    }
}
