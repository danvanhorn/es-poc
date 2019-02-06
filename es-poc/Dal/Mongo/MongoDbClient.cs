using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.IO;

namespace es_poc.Dal.Mongo
{
    public static class MongoDbClient
{
        public static void Init() {
            var client = new MongoClient("mongodb://root:example@mongo:27017");
            MongoCredential credential = MongoCredential.CreateCredential("ES-POC", "root", "example");

            IMongoDatabase db = client.GetDatabase("ES-POC");

            var collection = db.GetCollection<Data>("data");

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Dal", "Mongo", "data.json");

            Data[] list = JsonConvert.DeserializeObject<Data[]>(File.ReadAllText(path));

            collection.InsertMany(list);
        }

        private class Data
        {
            [BsonElement("text")]
            public string text { get; set; }
        }
    }
}
