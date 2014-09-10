using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectionJson
{
   public static class IEnumerable_Of_LinkExtensions
   {
       public static IEnumerable<Link> GetLinksByRel(this IEnumerable<Link> links, string rel)
       {
           return links.Where(l => l.Rel == rel);
       }

   }
}
