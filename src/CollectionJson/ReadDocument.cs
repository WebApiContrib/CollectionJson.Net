using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace CollectionJson
{
    [DataContract]
    public class ReadDocument : IReadDocument
    {
        public ReadDocument()
        {
            Collection = new Collection();
        }

        [DataMember(Name = "collection")]
        public Collection Collection { get; set; }
    }
}
