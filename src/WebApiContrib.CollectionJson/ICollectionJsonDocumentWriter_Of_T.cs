using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiContrib.CollectionJson
{
    public interface ICollectionJsonDocumentWriter<TItem>
    {
        ReadDocument Write(IEnumerable<TItem> data);
    }
}
