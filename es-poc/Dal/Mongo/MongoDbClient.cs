using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace es_poc.Dal.Mongo
{
    public static class MongoDbClient
{
        public static void Init() {
            var client = new MongoClient();
            IMongoDatabase db = client.GetDatabase("ES-POC");

            var collection = db.GetCollection<Data>("data");

            string path = Directory.GetCurrentDirectory();

            Data[] list = JsonConvert.DeserializeObject<Data[]>(File.ReadAllText(String.Concat(path, @"\Dal\Mongo\data.json")));

            collection.InsertMany(list);
        }

        private class Data
        {
            [BsonElement("text")]
            public string text { get; set; }
        }
    }
}
