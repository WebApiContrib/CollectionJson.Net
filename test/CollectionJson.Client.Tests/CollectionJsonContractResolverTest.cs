using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Should;
using Xunit;
using System.Reflection;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace CollectionJson.Client.Tests
{
    public class CollectionJsonContractResolverTest
    {
        private CollectionJsonContractResolver _resolver = new CollectionJsonContractResolver();
        private Func<MemberInfo, CollectionJsonContractResolver, MemberSerialization, JsonProperty> _createProperty;
        private PropertyInfo _itemsProperty;
        private PropertyInfo _versionProperty;

        public CollectionJsonContractResolverTest()
        {
            var method = _resolver.GetType().GetMethod("CreateProperty", BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.NonPublic);
            _createProperty = (m, r, s) =>
            {
                return (JsonProperty) method.Invoke(r, new object[] {m, s});
            };
            var collType = typeof(Collection);
            _itemsProperty = collType.GetProperty("Items", BindingFlags.Instance | BindingFlags.Public);
            _versionProperty = collType.GetProperty("Version", BindingFlags.Instance | BindingFlags.Public);
        }


        [Fact]
        public void WhenPropertyIsListAndItHasNoItemsItWillNotBeSerialized()
        {
            var collection = new Collection();
            var prop = _createProperty(_itemsProperty, _resolver, MemberSerialization.OptOut);
            prop.ShouldSerialize(collection).ShouldBeFalse();
        }
        
        [Fact]
        public void WhenPropertyIsListAndIsHasItemsItWillBeSerialized()
        {
            var collection = new Collection();
            collection.Items.Add(new Item());
            var prop = _createProperty(_itemsProperty, _resolver, MemberSerialization.OptOut);
            prop.ShouldSerialize(collection).ShouldBeTrue();
        }

        [Fact]
        public void WhenPropertyIsNotListItWillBeSerialized()
        {
            var collection = new Collection();
            var prop = _createProperty(_versionProperty, _resolver, MemberSerialization.OptOut);
            prop.ShouldSerialize(collection).ShouldBeTrue();
        }
    }
}
