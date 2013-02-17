using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiContrib.Formatting.CollectionJson
{
    public class Item
    {
        public Item()
        {
            Data = new List<Data>();
            Links = new List<Link>();
        }

        public Uri Href { get; set; }
        public IList<Data> Data { get; private set; }
        public IList<Link> Links { get; private set; }
        
    }
}
