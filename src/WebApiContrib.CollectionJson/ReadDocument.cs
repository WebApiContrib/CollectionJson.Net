using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiContrib.CollectionJson
{
    public class ReadDocument : IReadDocument
    {
        public ReadDocument()
        {
            Collection = new Collection();
        }

        public Collection Collection { get; set; }
    }
}
