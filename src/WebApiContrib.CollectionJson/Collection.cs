using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace WebApiContrib.CollectionJson
{
    [DataContract]
    public class Collection : ExtensibleObject
    {
        public Collection()
        {
            Links = new List<Link>();
            Items = new List<Item>();
            Queries = new List<Query>();
            Template = new Template();
        }

        [DataMember(Name="version")]
        public string Version
        {
            get { return GetValue<string>("Version"); }
            set { SetValue("Version", value); }
        }

        [DataMember(Name = "href")]
        public Uri Href
        {
            get { return GetValue<Uri>("Href"); }
            set { SetValue("Href", value); }
        }

        [DataMember(Name = "links")]
        public IList<Link> Links
        {
            get { return GetValue<IList<Link>>("Links"); }
            private set { SetValue("Links", value); }
        }

        [DataMember(Name = "items")]
        public IList<Item> Items
        {
            get { return GetValue<IList<Item>>("Items"); }
            private set { SetValue("Items", value); }
        }

        [DataMember(Name = "queries")]
        public IList<Query> Queries
        {
            get { return GetValue<IList<Query>>("Queries"); }
            private set { SetValue("Queries", value); }
        }

        [DataMember(Name = "template")]
        public Template Template
        {
            get { return GetValue<Template>("Template"); }
            set { SetValue("Template", value); }
        }
    }

}
