using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using DynamicUtils;

namespace CollectionJson
{
    [DataContract]
    public class Item : ExtensibleObject
    {
        public Item()
        {
            Data = new List<Data>();
            Links = new List<Link>();
        }

        [DataMember(Name = "href")]
        public Uri Href { get; set; }

        [DataMember(Name = "rel")]
        public string Rel { get; set; }

        [DataMember(Name = "rt")]
        public string Rt { get; set; }

        [DataMember(Name = "data")]
        public IList<Data> Data { get; set; }

        [DataMember(Name = "links")]
        public IList<Link> Links { get; set; }
        
    }
}
