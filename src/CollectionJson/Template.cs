using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CollectionJson
{
    [DataContract]
    public class Template : ExtensibleObject
    {
        public Template()
        {
            Data = new List<Data>();
        }

        [DataMember(Name = "Data")]
        public IList<Data> Data { get; set; }
    }
}
