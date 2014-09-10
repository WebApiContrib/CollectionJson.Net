using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectionJson
{
    public interface ICollectionJsonDocumentWriter<TItem>
    {
        IReadDocument Write(IEnumerable<TItem> data);
    }
}
