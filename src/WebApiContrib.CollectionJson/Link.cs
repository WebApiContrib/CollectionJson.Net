using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiContrib.CollectionJson
{
    public class Link : ExtensibleObject
    {
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

        public String Prompt
        {
            get { return GetValue<String>("Prompt"); }
            set { SetValue("Promot", value); }
        }

        public String Render
        {
            get { return GetValue<String>("Render"); }
            set { SetValue("Render", value); }
        }
    }
}
