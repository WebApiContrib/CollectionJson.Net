using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WebApiContrib.CollectionJson
{
    [DataContract]
    public class Link : ExtensibleObject
    {
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
        public String Prompt
        {
            get { return GetValue<String>("Prompt"); }
            set { SetValue("Promot", value); }
        }

        [DataMember(Name = "render")]
        public String Render
        {
            get { return GetValue<String>("Render"); }
            set { SetValue("Render", value); }
        }
    }
}
