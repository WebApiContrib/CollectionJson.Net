using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiContrib.CollectionJson
{
    public interface ICollectionJsonDocumentReader<TItem>
    {
        TItem Read(IWriteDocument document);
    }
}
