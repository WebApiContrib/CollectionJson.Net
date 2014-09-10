using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CollectionJson.Client
{
    public class CollectionJsonContent : HttpContent
    {
        private readonly ReadDocument _readDocument;
        private readonly JsonSerializer _serializer;

        public CollectionJsonContent(Collection collection)
        {
            _serializer = JsonSerializer.Create(new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = new CollectionJsonContractResolver()
            });

            collection.Version = "1.0";
            _readDocument = new ReadDocument();
            _readDocument.Collection = collection;

            Headers.ContentType = new MediaTypeHeaderValue("application/vnd.collection+json");
        }


        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            using (var writer = new JsonTextWriter(new StreamWriter(stream)) { CloseOutput = false })
            {
                _serializer.Serialize(writer, _readDocument);
                writer.Flush();
            }
            return Task.FromResult(0);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = -1;
            return false;
        }
    }
}
