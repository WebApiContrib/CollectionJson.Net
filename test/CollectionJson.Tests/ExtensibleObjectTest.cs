using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Dynamic;
using System.Linq;
using System.Text;
using Microsoft.CSharp.RuntimeBinder;
using Xunit;
using Should;

namespace CollectionJson.Tests
{
    public class ExtensibleObjectTest
    {
        public class TestObject : ExtensibleObject
        {
            public new IDictionary<string, object> Members
            {
                get
                {
                    return base.Members;
                }
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                Found = base.TryGetMember(binder, out result);
                Result = result;
                return Found;
            }

            public object Result { get; set; }
            public bool Found { get; set; }
        }

        public class ExtensibleObjectTestCommon
        {
            protected TestObject _obj;
            protected dynamic _dObj;

            public ExtensibleObjectTestCommon()
            {
                _obj = new TestObject();
                _dObj = _obj;
                _dObj.Test1 = "TestValue";
                _dObj.Test2 = null;
            }
        }

        public class TheTrySetMemberMethod : ExtensibleObjectTestCommon
        {
            [Fact]
            public void WhenPassedAValueThenShouldStoreIt()
            {
                object value;
                var found = _obj.Members.TryGetValue("Test1", out value);
                found.ShouldBeTrue();
                value.ShouldEqual("TestValue");
            }
        }

        public class TheTryGetMemberMethod : ExtensibleObjectTestCommon
        {
            [Fact]
            public void WhenValueIsStoredAndIsNotNullThenShouldReturnTrue()
            {
                var test = _dObj.Test1;
                _obj.Found.ShouldBeTrue();
            }

            [Fact]
            public void WhenValueIsStoredAndIsNullThenShouldThrowException()
            {
                Assert.Throws<RuntimeBinderException>(() => 
                {
                    var test = _dObj.Test2;
                });
            }

            [Fact]
            public void WhenValueIsStoredThenShouldReturnIt()
            {
                var test = (string) _dObj.Test1;
                test.ShouldEqual("TestValue");
            }
        }

        public class TheGetDynamicMemberNamesMethod : ExtensibleObjectTestCommon
        {
            [Fact]
            public void WhenMembersExistThenShouldReturnTheNames()
            {
                var names = _obj.GetDynamicMemberNames();
                names.Count().ShouldEqual(1);
                names.ShouldContain("Test1");
            }
        }

        public class TheGetValueMethod : ExtensibleObjectTestCommon
        {
            [Fact]
            public void WhenMemberExistsAndIsNotNullThenShouldReturnCastedValue()
            {
                var test = _obj.GetValue<string>("Test1");
                test.ShouldEqual("TestValue");
            }

            [Fact]
            public void WhenMemberDoesNotExistThenShouldReturnDefaultForType()
            {
                var num = _obj.GetValue<int>("Number");
                num.ShouldEqual(0);
            }
        }

        public class TheSetValueMethod : ExtensibleObjectTestCommon
        {
            public TheSetValueMethod()
            {
                _obj.SetValue("Test3", "TestValue");
            }

            [Fact]
            public void WhenValueIsPassedThenShouldStoreTheValue()
            {
                _obj.Members["Test3"].ShouldEqual("TestValue");
            }
        }


    }
}
