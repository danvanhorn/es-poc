using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace es_poc.Models
{
    public class SearchModel
{
        public string query{ get; set; }

        public List<SearchResultViewModel> models { get; set; }
}

}
