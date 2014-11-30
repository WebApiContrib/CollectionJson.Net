using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using DynamicUtils;

namespace CollectionJson
{
    [DataContract]
    public class Link : ExtensibleObject
    {
        [DataMember(Name = "rel")]
        public String Rel { get; set; }

        [DataMember(Name = "href")]
        public Uri Href { get; set; }

        [DataMember(Name = "prompt")]
        public String Prompt { get; set; }

        [DataMember(Name = "render")]
        public String Render { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
