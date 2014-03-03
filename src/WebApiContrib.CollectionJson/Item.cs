using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiContrib.CollectionJson
{
    public class Item : ExtensibleObject
    {
        public Item()
        {
            Data = new List<Data>();
            Links = new List<Link>();
        }

        public Uri Href
        {
            get { return GetValue<Uri>("Href"); }
            set { SetValue("Href", value); }
        }

        public string Rel
        {
            get { return GetValue<string>("Rel"); }
            set { SetValue("Rel", value); }
        }

        public string Rt
        {
            get { return GetValue<string>("Rt"); }
            set { SetValue("Rt", value); }
        }

        public IList<Data> Data
        {
            get { return GetValue<IList<Data>>("Data"); }
            private set { SetValue("Data", value); }
        }

        public IList<Link> Links
        {
            get { return GetValue<IList<Link>>("Links"); }
            private set { SetValue("Links", value); }
        }
        
    }
}
