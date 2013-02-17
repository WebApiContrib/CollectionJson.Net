using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Should;
using WebApiContrib.Formatting.CollectionJson;

namespace WebApiContrib.Formatting.CollectionJson.Tests
{
    public class ExtensiontsTests
    {
        [Fact]
        public void WhenRetrievingLinkByRelAndLinkIsPresentShouldReturnIt()
        {
            var links = new List<Link>
                {
                    new Link {Rel = "Foo"},
                    new Link {Rel = "Bar"}
                };

            links.GetLinksByRel("Foo").Count(l=>l.Rel == "Foo").ShouldEqual(1);
        }

        [Fact]
        public void WhenRetrievingDataElementByNameAndDataIsPresentShouldReturnIt()
        {
            var data = new List<Data>
                {
                    new Data {Name = "Foo"},
                    new Data {Name = "Bar"}
                };

            data.GetDataByName("Foo").Name.ShouldEqual("Foo");
        }


    }
}
