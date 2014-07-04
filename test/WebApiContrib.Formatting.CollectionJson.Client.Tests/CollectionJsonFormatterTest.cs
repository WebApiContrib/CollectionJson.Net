using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebApiContrib.CollectionJson;
using Xunit;
using Should;

namespace WebApiContrib.Formatting.CollectionJson.Client.Tests
{
    public class CollectionJsonFormatterTest
    {
        private CollectionJsonFormatter formatter = new CollectionJsonFormatter();

 		[Fact]
        public void WhenTypeIsWriteDocumentShouldBeAbleToRead()
        {
            formatter.CanReadType(typeof(WriteDocument)).ShouldBeTrue();
        }

        [Fact]
        public void WhenTypeIsWriteDocumentShouldBeAbleToWrite()
        {
            formatter.CanWriteType(typeof(WriteDocument)).ShouldBeTrue();
        }


		[Fact]
        public void WhenTypeIsReadDocumentShouldBeAbleToRead()
        {
            formatter.CanReadType(typeof(ReadDocument)).ShouldBeTrue();
        }

        [Fact]
        public void WhenTypeIsReadDocumentShouldBeAbleToWrite()
        {
            formatter.CanWriteType(typeof(ReadDocument)).ShouldBeTrue();
        }


        [Fact]
        public void WhenTypeIsStringShouldNotBeAbleToRead()
        {
            formatter.CanReadType(typeof(string)).ShouldBeFalse();
        }

        [Fact]
        public void WhenTypeIsStringShouldNotBeAbleToWrite()
        {
            formatter.CanReadType(typeof(string)).ShouldBeFalse();
        }

        [Fact]
        public void WhenInitializedShouldSetSupportedMediaTpeToCollectionJson()
        {
            formatter.SupportedMediaTypes.Count(m => m.MediaType=="application/vnd.collection+json").ShouldEqual(1);
        }

        [Fact]
        public void WhenInitializedShouldSetCamelCasePropertyResolver()
        {
            formatter.SerializerSettings.ContractResolver.ShouldBeType<CamelCasePropertyNamesContractResolver>();
        }

        [Fact]
        public void WhenInitializedShouldSetIndentation()
        {
            formatter.SerializerSettings.Formatting.ShouldEqual(Newtonsoft.Json.Formatting.Indented);
        }

        [Fact]
        public void WhenInitializedShouldIgnoreNulls()
        {
            formatter.SerializerSettings.NullValueHandling.ShouldEqual(NullValueHandling.Ignore);
        }

        [Fact]
        public void WhenTypeImplementsIReadDocumentThenWriteToStreamAsyncWritesCollectionJson()
        {
            var doc = new TestReadDocument();
            var stream = new MemoryStream();
            formatter.WriteToStreamAsync(doc.GetType(), doc, stream, null, null).Wait();
            var reader = new StreamReader(stream);
            stream.Position = 0;
            var content = reader.ReadToEnd();
            content.ShouldContain("\"collection\":");
            content.ShouldContain("\"http://test.com\"");
        }

        [Fact]
        public void WhenTypeImplementsIWriteDocumentThenWriteToStreamAsyncWritesCollectionJson()
        {
            var doc = new TestWriteDocument();
            var stream = new MemoryStream();
            formatter.WriteToStreamAsync(doc.GetType(), doc, stream, null, null).Wait();
            var reader = new StreamReader(stream);
            stream.Position = 0;
            var content = reader.ReadToEnd();
            content.ShouldContain("\"TestValue\"");
        }

        public class TestReadDocument : IReadDocument
        {
            public TestReadDocument()
            {
                _collection = new Collection();
                _collection.Href = new Uri("http://test.com");
            }

            private Collection _collection;

            Collection IReadDocument.Collection
            {
                get
                {
                    return _collection;
                }
            }
        }

        public class TestWriteDocument : IWriteDocument
        {
            public TestWriteDocument()
            {
                _template = new Template();
                _template.Data.Add(new Data{Name="Value", Value="TestValue"});
            }

            private Template _template;

            Template IWriteDocument.Template
            {
                get
                {
                    return _template;
                }
            }
        }

    }
}
