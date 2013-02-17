using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiContrib.Formatting.CollectionJson
{
    public class Template
    {
        public Template()
        {
            Data = new List<Data>();
        }

        public IList<Data> Data { get; set; }
    }
}
