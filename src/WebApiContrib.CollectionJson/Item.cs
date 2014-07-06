using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WebApiContrib.CollectionJson
{
    [DataContract]
    public class Item : ExtensibleObject
    {
        public Item()
        {
            Data = new List<Data>();
            Links = new List<Link>();
        }

        [DataMember(Name = "href")]
        public Uri Href
        {
            get { return GetValue<Uri>("Href"); }
            set { SetValue("Href", value); }
        }

        [DataMember(Name = "rel")]
        public string Rel
        {
            get { return GetValue<string>("Rel"); }
            set { SetValue("Rel", value); }
        }

        [DataMember(Name = "rt")]
        public string Rt
        {
            get { return GetValue<string>("Rt"); }
            set { SetValue("Rt", value); }
        }

        [DataMember(Name = "data")]
        public IList<Data> Data
        {
            get { return GetValue<IList<Data>>("Data"); }
            private set { SetValue("Data", value); }
        }

        [DataMember(Name = "links")]
        public IList<Link> Links
        {
            get { return GetValue<IList<Link>>("Links"); }
            private set { SetValue("Links", value); }
        }
        
    }
}
