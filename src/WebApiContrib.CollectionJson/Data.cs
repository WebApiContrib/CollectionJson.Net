using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WebApiContrib.CollectionJson
{
    [DataContract]
    public class Data : ExtensibleObject
    {
        [DataMember(Name = "name")]
        public string Name
        {
            get { return GetValue<string>("Name"); }
            set { SetValue("Name", value); }
        }

        [DataMember(Name = "value")]
        public string Value
        {
            get { return GetValue<string>("Value"); }
            set { SetValue("Value", value); }
        }

        [DataMember(Name = "prompt")]
        public string Prompt
        {
            get { return GetValue<string>("Prompt"); }
            set { SetValue("Prompt", value); }
        }
    }
}
