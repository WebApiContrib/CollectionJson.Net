using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using DynamicUtils;

namespace CollectionJson
{
    [DataContract]
    public class Query : ExtensibleObject
    {
        public Query()
        {
            Data = new List<Data>();
        }

        [DataMember(Name = "rt")]
        public String Rt { get; set; }

        [DataMember(Name = "rel")]
        public String Rel { get; set; }

        [DataMember(Name = "href")]
        public Uri Href { get; set; }

        [DataMember(Name = "prompt")]
        public string Prompt { get; set; }

        [DataMember(Name = "data")]
        public IList<Data> Data { get; set; }
    }
}
