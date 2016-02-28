using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using DynamicUtils;

namespace CollectionJson
{
    [DataContract]
    public class Collection : ExtensibleObject
    {
        public const string MediaType = "application/vnd.collection+json";

        public Collection()
        {
            Links = new List<Link>();
            Items = new List<Item>();
            Queries = new List<Query>();
            Template = new Template();
        }

        [DataMember(Name="version")]
        public string Version { get; set; }
 
        [DataMember(Name = "href")]
        public Uri Href { get; set; }

        [DataMember(Name = "links")]
        public IList<Link> Links { get; private set; }
 
        [DataMember(Name = "items")]
        public IList<Item> Items { get; private set; }

        [DataMember(Name = "queries")]
        public IList<Query> Queries { get; private set; }
 
        [DataMember(Name = "template")]
        public Template Template { get; private set; }

        [DataMember(Name = "error")]
        public Error Error { get; set; }
    }

}
