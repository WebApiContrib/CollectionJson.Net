using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CollectionJson
{
    [DataContract]
    public class WriteDocument : IWriteDocument
    {
        [DataMember(Name = "template")]
        public Template Template { get; set; }
    }
}
