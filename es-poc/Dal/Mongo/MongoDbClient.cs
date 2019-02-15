using es_poc.Dal.Entities;
using es_poc.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace es_poc.Dal.Mongo
{
    public class MongoDbClient : IDisposable
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

            if (db.GetCollection<Data>(mCollection) != null) {
                db.DropCollection(mCollection);
            }

            IMongoCollection<Data> collection = db.GetCollection<Data>(mCollection);

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Dal", "Mongo", "data.json");

            Data[] list = JsonConvert.DeserializeObject<Data[]>(File.ReadAllText(path));

            collection.InsertMany(list);
        }

        public List<Data> Query(string id)
        {
            IMongoDatabase db = mClient.GetDatabase(mDb);
            IMongoCollection<Data> collection = db.GetCollection<Data>(mCollection);

            return collection.Find(new BsonDocument { { "_id", new ObjectId(id) } }).ToList();
        }

        public List<Data> QueryAll()
        {
            IMongoDatabase db = mClient.GetDatabase(mDb);
            IMongoCollection<Data> collection = db.GetCollection<Data>(mCollection);

            return collection.Find(_ => true).ToList();
        }

        public void Dispose()
        {
            // throw new NotImplementedException();
        }
    }
}
