using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using CollectionJson;
using Xunit;
using Should;

namespace CollectionJson.Client.Tests
{
    public class ReadDocumentExtensionsTest
    {
        [Fact]
        public void WhenToObjectContentIsInvokedThenObjectContentIsReturned()
        {
            var document = new ReadDocument();
            var content = document.ToObjectContent() as ObjectContent<IReadDocument>;
            content.ShouldNotBeNull();
            content.Value.ShouldEqual(document);
        }

        [Fact]
        public void WhenToHttpResponseMessageIsInvokedOnReadDocumentThenResponseMessageIsReturned()
        {
            var document = new ReadDocument();
            var response = document.ToHttpResponseMessage();
            response.ShouldNotBeNull();
            response.Content.ShouldNotBeNull();
            response.Content.ShouldBeType<ObjectContent<IReadDocument>>();
        }
    }
}
