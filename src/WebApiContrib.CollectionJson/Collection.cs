using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiContrib.CollectionJson
{
    public class Collection : ExtensibleObject
    {
        public Collection()
        {
            Links = new List<Link>();
            Items = new List<Item>();
            Queries = new List<Query>();
            Template = new Template();
        }

        public string Version
        {
            get { return GetValue<string>("Version"); }
            set { SetValue("Version", value); }
        }

        public Uri Href
        {
            get { return GetValue<Uri>("Href"); }
            set { SetValue("Href", value); }
        }

        public IList<Link> Links
        {
            get { return GetValue<IList<Link>>("Links"); }
            private set { SetValue("Links", value); }
        }

        public IList<Item> Items
        {
            get { return GetValue<IList<Item>>("Items"); }
            private set { SetValue("Items", value); }
        }

        public IList<Query> Queries
        {
            get { return GetValue<IList<Query>>("Query"); }
            private set { SetValue("Query", value); }
        }

        public Template Template
        {
            get { return GetValue<Template>("Template"); }
            set { SetValue("Template", value); }
        }
    }

}
