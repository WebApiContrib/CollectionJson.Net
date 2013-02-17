using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiContrib.Formatting.CollectionJson
{
    public interface ICollectionJsonDocumentWriter<TItem>
    {
        WebApiContrib.Formatting.CollectionJson.ReadDocument Write(IEnumerable<TItem> data);
    }
}
