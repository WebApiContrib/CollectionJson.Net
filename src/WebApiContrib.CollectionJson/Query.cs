using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiContrib.CollectionJson
{
    public class Query : ExtensibleObject
    {
        public Query()
        {
            Data = new List<Data>();
        }

        public String Rt
        {
            get { return GetValue<String>("Rt"); }
            set { SetValue("Rt", value); }
        }

        public String Rel
        {
            get { return GetValue<String>("Rel"); }
            set { SetValue("Rel", value); }
        }

        public Uri Href
        {
            get { return GetValue<Uri>("Href"); }
            set { SetValue("Href", value); }
        }

        public string Prompt
        {
            get { return GetValue<String>("Prompt"); }
            set { SetValue("Prompt", value); }
        }

        public IList<Data> Data
        {
            get { return GetValue<IList<Data>>("Data"); }
            set { SetValue("Data", value); }
        }
    }
}
