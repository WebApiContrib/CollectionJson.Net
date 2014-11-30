using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DynamicUtils;

namespace CollectionJson
{
    public abstract class CollectionJsonDocumentWriter<TItem> : ICollectionJsonDocumentWriter<TItem>
    {
        public virtual IReadDocument Write(IEnumerable<TItem> data, Uri uri)
        {
            throw new NotImplementedException();
        }

        public virtual IReadDocument Write(IEnumerable<TItem> data)
        {
            throw new NotImplementedException();
        }
    }
}
