using es_poc.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace es_poc.Models
{
    public class SearchDetailsViewModel : SearchResultViewModel
{
    public int additionalProperty { get; set; }

    public string imageFilename { get; set; }

    public SearchDetailsViewModel(string _id, string text, int additionalProperty, string imageFilename) : base(_id, text)
    {
        this.additionalProperty = additionalProperty;
        this.imageFilename = imageFilename;
    }

    public static explicit operator SearchDetailsViewModel(Data v)
    {
        return new SearchDetailsViewModel(v._id, v.text, v.additionalProperty, v.imageFilename);
    }
}
}
