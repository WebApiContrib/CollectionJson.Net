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
using CollectionJson;
using Xunit;
using Should;

namespace CollectionJson.Client.Tests
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
        public void WhenInitializedShouldSetCollectionJsonContractResolver()
        {
            formatter.SerializerSettings.ContractResolver.ShouldBeType<CollectionJsonContractResolver>();
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
        public async Task WhenTypeImplementsIReadDocumentThenWriteToStreamAsyncWritesCollectionJson()
        {
            var doc = new TestReadDocument();
            var stream = new MemoryStream();
            await formatter.WriteToStreamAsync(doc.GetType(), doc, stream, null, null);
            var reader = new StreamReader(stream);
            stream.Position = 0;
            var content = reader.ReadToEnd();
            content.ShouldContain("\"collection\":");
            content.ShouldContain("\"http://test.com\"");
        }

        [Fact]
        public async Task WhenTypeImplementsIWriteDocumentThenWriteToStreamAsyncWritesCollectionJson()
        {
            var doc = new TestWriteDocument();
            var stream = new MemoryStream();
            await formatter.WriteToStreamAsync(doc.GetType(), doc, stream, null, null);
            var reader = new StreamReader(stream);
            stream.Position = 0;
            var content = await reader.ReadToEndAsync();
            content.ShouldContain("\"TestValue\"");
        }

        [Fact]
        public async Task WhenTypeImplementsIWriteDocumentThenReadToStreamAsyncReturnsWriteDocument()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            await writer.WriteLineAsync("{\"template\":{\"data\":[{\"name\":\"value\"}]}}");
            await writer.FlushAsync();
            var content = new StreamContent(stream);
            content.Headers.ContentLength = stream.Position-1;
            stream.Position = 0;
            content.Headers.ContentType = new MediaTypeHeaderValue(Collection.MediaType);
            var doc = (IWriteDocument) await formatter.ReadFromStreamAsync(typeof (IWriteDocument), stream, content, null);
            doc.ShouldNotBeNull();
            doc.Template.ShouldNotBeNull();
            doc.Template.Data.GetDataByName("value").ShouldNotBeNull();
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
