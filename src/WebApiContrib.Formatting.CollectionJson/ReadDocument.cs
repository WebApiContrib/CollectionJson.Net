using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiContrib.Formatting.CollectionJson
{
    public class ReadDocument
    {
        public ReadDocument()
        {
            Collection = new Collection();
        }

        public Collection Collection { get; private set; }
    }
}
