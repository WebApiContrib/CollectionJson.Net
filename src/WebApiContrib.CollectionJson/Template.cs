using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WebApiContrib.CollectionJson
{
    [DataContract]
    public class Template
    {
        public Template()
        {
            Data = new List<Data>();
        }

        [DataMember(Name = "Data")]
        public IList<Data> Data { get; set; }
    }
}
