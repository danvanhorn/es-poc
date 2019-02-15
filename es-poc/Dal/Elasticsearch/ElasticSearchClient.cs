using es_poc.Dal.Entities;
using es_poc.Dal.Mongo;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static es_poc.Dal.Mongo.MongoDbClient;

namespace es_poc.Dal.ElasticSearch
{
    public class ElasticSearchClient
    {
        private ElasticClient mClient;
        private const string mIndexName = "searchdata";

        public ElasticSearchClient()
        {
            Uri uri = new Uri("http://elasticsearch:9200");
            var settings = new ConnectionSettings(uri)
                .DisableDirectStreaming()
                .DefaultIndex(mIndexName)
                .DefaultMappingFor<SearchData>(i => i
                    .PropertyName(d => d._id, "id")
                    .PropertyName(d => d.text, "text")
                );

            mClient = new ElasticClient(settings);
        }

        public void CreateIndex()
        {
            mClient.CreateIndex(mIndexName, i => i
                .Mappings(ms => ms
                    .Map<SearchData>(m => m.AutoMap()
                    )
                )
            );
        }

        public void Index()
        {
            mClient.DeleteIndex(mIndexName);
            CreateIndex();

            using (MongoDbClient client = new MongoDbClient())
            {
                IList<SearchData> docs = client.QueryAll().Cast<SearchData>().ToList();
                foreach (Data data in docs)
                {
                    var indexResponse = mClient.IndexDocument((SearchData)data);
                }
            }
        }

        public IEnumerable<SearchData> Search(string query)
        {
            var response = mClient.Search<SearchData>(d => d
                .Query(q => q
                    .MatchPhrase(m => m
                        .Field(f => f.text)
                        .Query(query)
                    )
                )
            );

            return response.Documents.ToList();
        }

    }
}
