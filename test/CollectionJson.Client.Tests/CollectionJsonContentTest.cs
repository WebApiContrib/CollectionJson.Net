using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Should;
using Xunit;

namespace CollectionJson.Client.Tests
{
    public class CollectionJsonContentTest
    {
        [Fact]
        public async void WhenCreatingCollectionJsonContentObjectIsSerializedToCollectionJson()
        {
            var coll = new Collection();
            var content = new CollectionJsonContent(coll);
            var stream = new MemoryStream();
            await content.CopyToAsync(stream);
            var reader = new StreamReader(stream);
            stream.Position = 0;
            var json = reader.ReadToEnd();
            json.ShouldContain("\"collection\"");
        }

        [Fact]
        public async void WhenCreatingCollectionJsonContentWithErrorObjectIsSerializedWithError()
        {
            var coll = new Collection()
            {
                Error = new Error()
                {
                    Code = "1234",
                    Message = "Hello world",
                    Title = "An error occurred"
                }
            };
            var content = new CollectionJsonContent(coll);
            var stream = new MemoryStream();
            await content.CopyToAsync(stream);
            var reader = new StreamReader(stream);
            stream.Position = 0;
            var json = reader.ReadToEnd();
            json.ShouldContain("\"error\"");
        }
    }
}
