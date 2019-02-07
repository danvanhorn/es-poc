using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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
            mClient = new MongoClient(mConnectionString);
            MongoCredential credential = MongoCredential.CreateCredential(mDb, mUser, mPassword);
            //MongoClientSettings mongoCredentialSettings = new MongoClientSettings()
            //{
            //    Credential =  credential ,
            //    Server = new MongoServerAddress("mongodb://localhost", 27017)
            //};
            //mClient = new MongoClient(mongoCredentialSettings);
        }

        public void Init() {
            IMongoDatabase db = mClient.GetDatabase(mDb);
            IMongoCollection<BsonBinaryData> collection = db.GetCollection<BsonBinaryData>(mCollection);

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Dal", "Mongo", "data.json");

            BsonBinaryData[] list = JsonConvert.DeserializeObject<BsonBinaryData[]>(File.ReadAllText(path));

            collection.InsertMany(list);
        }

        public BsonDocument Query(string id)
        {
            IMongoDatabase db = mClient.GetDatabase(mDb);
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(mCollection);
            
            // We're getting a time out error here.
            var foo = collection.Find(new BsonDocument { { "_id", new ObjectId(id) } }).FirstAsync().Result;
            return foo;
        }


        private class Data
        {
            [BsonElement("text")]
            public string text { get; set; }
        }
    }
}
