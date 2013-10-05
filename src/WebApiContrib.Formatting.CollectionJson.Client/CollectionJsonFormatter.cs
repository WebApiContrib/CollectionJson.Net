using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using WebApiContrib.CollectionJson;

namespace WebApiContrib.Formatting.CollectionJson.Client
{
    public class CollectionJsonFormatter : JsonMediaTypeFormatter
    {
        public CollectionJsonFormatter()
        {
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.collection+json"));
            SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();
        }

        public override bool CanWriteType(Type type)
        {
            return base.CanWriteType(type) && (typeof(IReadDocument).IsAssignableFrom(type) || typeof (IWriteDocument).IsAssignableFrom(type));
        }

        public override bool CanReadType(Type type)
        {
            return base.CanReadType(type) && (typeof(IReadDocument).IsAssignableFrom(type) || typeof(IWriteDocument).IsAssignableFrom(type));
        }

        public override System.Threading.Tasks.Task<object> ReadFromStreamAsync(Type type, Stream readStream, System.Net.Http.HttpContent content, IFormatterLogger formatterLogger)
        {
            if (typeof(IReadDocument).IsAssignableFrom(type) && type != typeof(ReadDocument))
            {
                return base.ReadFromStreamAsync(typeof(IReadDocument), readStream, content, formatterLogger);
            }
            
            if (typeof(IWriteDocument).IsAssignableFrom(type) && type != typeof(WriteDocument))
            {
                return base.ReadFromStreamAsync(typeof(IWriteDocument), readStream, content, formatterLogger);
            }

            return base.ReadFromStreamAsync(type, readStream, content, formatterLogger);
        }

        public override System.Threading.Tasks.Task WriteToStreamAsync(Type type, object value, Stream writeStream, System.Net.Http.HttpContent content, System.Net.TransportContext transportContext)
        {
            if (typeof (IReadDocument).IsAssignableFrom(type) && type != typeof(ReadDocument))
            {
                return base.WriteToStreamAsync(typeof(IReadDocument), new ReadDocumentDecorator((IReadDocument) value), writeStream, content, transportContext);
            }
            
            if (typeof (IWriteDocument).IsAssignableFrom(type) && type != typeof(WriteDocument))
            {
                return base.WriteToStreamAsync(typeof(IWriteDocument), new WriteDocumentDecorator((IWriteDocument) value), writeStream, content, transportContext);
            }

            return base.WriteToStreamAsync(type, value, writeStream, content, transportContext);
        }

        private class ReadDocumentDecorator : IReadDocument
        {
            private readonly IReadDocument _innerReadDocument;

            public ReadDocumentDecorator(IReadDocument innerReadDocument)
            {
                _innerReadDocument = innerReadDocument;
            }

            public Collection Collection
            {
                get { return _innerReadDocument.Collection; }
            }
        }
        
        private class WriteDocumentDecorator : IWriteDocument
        {
            private readonly IWriteDocument _innerWriteDocument;

            public WriteDocumentDecorator(IWriteDocument innerWriteDocument)
            {
                _innerWriteDocument = innerWriteDocument;
            }

            public Template Template
            {
                get { return _innerWriteDocument.Template; }
            }
        }

    }
}
