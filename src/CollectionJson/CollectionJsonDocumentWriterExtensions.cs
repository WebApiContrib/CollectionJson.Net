using System;

namespace CollectionJson
{
    public static class CollectionJsonDocumentWriterExtensions
    {
        public static IReadDocument Write<TItem>(this ICollectionJsonDocumentWriter<TItem> writer, TItem item)
        {
            return writer.Write(new[] { item });
        }

        public static IReadDocument Write<TItem>(this CollectionJsonDocumentWriter<TItem> writer, TItem item, Uri uri)
        {
            return writer.Write(new[] { item }, uri);
        }
    }
}