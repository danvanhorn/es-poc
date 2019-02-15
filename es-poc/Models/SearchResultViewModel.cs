using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using es_poc.Dal.Entities;

namespace es_poc.Models
{
    public class SearchResultViewModel
{
        public string _id { get; set; }
        public string text { get; set; }

        public SearchResultViewModel(string _id, string text)
        {
            this._id = _id;
            this.text = text;
        }

        public static explicit operator SearchResultViewModel(SearchData v)
        {
            return new SearchResultViewModel(v._id, v.text);
        }
    }
}
