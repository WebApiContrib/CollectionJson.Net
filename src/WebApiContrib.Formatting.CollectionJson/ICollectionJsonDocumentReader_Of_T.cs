using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiContrib.Formatting.CollectionJson
{
    public interface ICollectionJsonDocumentReader<TItem>
    {
        TItem Read(WriteDocument document);
    }
}
