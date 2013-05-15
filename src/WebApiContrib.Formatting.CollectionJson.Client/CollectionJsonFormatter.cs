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

namespace WebApiContrib.Formatting.CollectionJson
{
    public class CollectionJsonFormatter : JsonMediaTypeFormatter
    {
        public CollectionJsonFormatter()
        {
            this.SupportedMediaTypes.Clear();
            this.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.collection+json"));
            this.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            this.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            this.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();
        }

        public override bool CanWriteType(Type type)
        {
            return (type == typeof(ReadDocument) || type == typeof(WriteDocument));
        }

        public override bool CanReadType(Type type)
        {
            return (type == typeof(ReadDocument) || type == typeof(WriteDocument));
        }
    }
}
