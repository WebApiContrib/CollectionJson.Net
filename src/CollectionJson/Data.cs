using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CollectionJson
{
    [DataContract]
    public class Data : ExtensibleObject
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        [DataMember(Name = "prompt")]
        public string Prompt { get; set; }

    }
}
