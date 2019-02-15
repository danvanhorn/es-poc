using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace es_poc.Dal.Entities
{
    public class SearchData : BaseData
    {
        [BsonElement("text")]
        public string text { get; set; }

    }
}
