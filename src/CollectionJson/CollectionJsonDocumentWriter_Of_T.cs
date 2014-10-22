using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectionJson
{
    public abstract class CollectionJsonDocumentWriter<TItem> : ICollectionJsonDocumentWriter<TItem>
    {
        public virtual IReadDocument Write(IEnumerable<TItem> data, Uri uri)
        {
            return null;
        }

        public virtual IReadDocument Write(IEnumerable<TItem> data)
        {
            return null;
        }
    }
}
