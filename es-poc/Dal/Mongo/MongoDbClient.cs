using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace es_poc.Dal.Mongo
{
    public static class MongoDbClient
{
        public static void initialize() {
            var client = new MongoClient("http://localhost:8081");



        }

}
}
