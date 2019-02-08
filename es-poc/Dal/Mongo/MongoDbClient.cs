using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace es_poc.Dal.Mongo
{
    public class MongoDbClient
    {
        private static IMongoClient mClient;
        private const string mDb = "es-poc";
        private const string mCollection = "data";
        private const string mConnectionString = "mongodb://root:example@mongo:27017";
        private const string mUser = "root";
        private const string mPassword = "example";

        public MongoDbClient() {
            // for docker
            mClient = new MongoClient(mConnectionString);
            // for local
            // mClient = new MongoClient();
            MongoCredential credential = MongoCredential.CreateCredential(mDb, mUser, mPassword);
        }

        public void Init() {
            IMongoDatabase db = mClient.GetDatabase(mDb);
            IMongoCollection<Data> collection = db.GetCollection<Data>(mCollection);

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Dal", "Mongo", "data.json");

            Data[] list = JsonConvert.DeserializeObject<Data[]>(File.ReadAllText(path));

            collection.InsertMany(list);
        }

        public Data Query(string id)
        {
            IMongoDatabase db = mClient.GetDatabase(mDb);
            IMongoCollection<Data> collection = db.GetCollection<Data>(mCollection);

            return collection.Find(new BsonDocument { { "_id", new ObjectId(id) } }).FirstAsync().Result;;
        }

        public List<Data> QueryAll()
        {
            IMongoDatabase db = mClient.GetDatabase(mDb);
            IMongoCollection<Data> collection = db.GetCollection<Data>(mCollection);

            return collection.Find(_ => true).ToList();
        } 


        public class Data
        {

            [BsonIgnoreIfNull]
            public ObjectId? _id { get; set; }

            [BsonElement("text")]
            public string text { get; set; }
        }
    }
}
