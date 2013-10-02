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
    }
}
