using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit;
using Should;

namespace WebApiContrib.Formatting.CollectionJson.Tests
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
	
    }

}
