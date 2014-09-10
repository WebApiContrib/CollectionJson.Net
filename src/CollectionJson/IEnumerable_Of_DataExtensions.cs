using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectionJson
{
    public static class IEnumerable_Of_DataExtensions
    {
        public static Data GetDataByName(this IEnumerable<Data> data, string name)
        {
            return data.Single(d => d.Name == name);
        }
    }
}
