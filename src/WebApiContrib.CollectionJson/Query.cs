using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WebApiContrib.CollectionJson
{
    [DataContract]
    public class Query : ExtensibleObject
    {
        public Query()
        {
            Data = new List<Data>();
        }

        [DataMember(Name = "rt")]
        public String Rt
        {
            get { return GetValue<String>("Rt"); }
            set { SetValue("Rt", value); }
        }

        [DataMember(Name = "rel")]
        public String Rel
        {
            get { return GetValue<String>("Rel"); }
            set { SetValue("Rel", value); }
        }

        [DataMember(Name = "href")]
        public Uri Href
        {
            get { return GetValue<Uri>("Href"); }
            set { SetValue("Href", value); }
        }

        [DataMember(Name = "prompt")]
        public string Prompt
        {
            get { return GetValue<String>("Prompt"); }
            set { SetValue("Prompt", value); }
        }

        [DataMember(Name = "data")]
        public IList<Data> Data
        {
            get { return GetValue<IList<Data>>("Data"); }
            set { SetValue("Data", value); }
        }
    }
}
