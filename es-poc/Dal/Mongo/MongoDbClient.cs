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
        }

        public void Init() {
            IMongoDatabase db = mClient.GetDatabase(mDb);
            IMongoCollection<Data> collection = db.GetCollection<Data>(mCollection);

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Dal", "Mongo", "data.json");

            Data[] list = JsonConvert.DeserializeObject<Data[]>(File.ReadAllText(path));

            collection.InsertMany(list);
        }

        public async Task Query(string id) {
            IMongoDatabase db = mClient.GetDatabase(mDb);
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(mCollection);

            //using (IAsyncCursor<BsonDocument> cursor = await collection.FindAsync(new BsonDocument("_id", BsonObjectId(id))))
            //{
            //    while (await cursor.MoveNextAsync())
            //    {
            //        IEnumerable<BsonDocument> batch = cursor.Current;

            //    }
            //}
        }


        private class Data
        {
            [BsonElement("text")]
            public string text { get; set; }
        }
    }
}
