using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiContrib.CollectionJson
{
    public class Data : ExtensibleObject
    {
        public string Name
        {
            get { return GetValue<string>("Name"); }
            set { SetValue("Name", value); }
        }

        public string Value
        {
            get { return GetValue<string>("Value"); }
            set { SetValue("Value", value); }
        }

        public string Prompt
        {
            get { return GetValue<string>("Prompt"); }
            set { SetValue("Prompt", value); }
        }
    }
}
