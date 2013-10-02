namespace WebApiContrib.CollectionJson
{
    public static class CollectionJsonDocumentWriterExtensions
    {
        public static IReadDocument Write<TItem>(this ICollectionJsonDocumentWriter<TItem> writer, TItem item)
        {
            return writer.Write(new[] {item});
        }
    }
}