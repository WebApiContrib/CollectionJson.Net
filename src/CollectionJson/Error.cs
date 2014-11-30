using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using DynamicUtils;

namespace CollectionJson
{
    [DataContract]
    public class Error : ExtensibleObject
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}
