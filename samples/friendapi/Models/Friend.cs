using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WebApiContrib.Formatting.CollectionJson.Models
{
    public class Friend
    {
        private string fullName;
        

        public int Id { get; set; }
        public string ShortName { get; private set;}
        public string FullName {
            get { return fullName; }
            set
            {
                fullName = value;
                var tempName = fullName.ToLower();
                ShortName = tempName.Substring(0, 1) + tempName.Substring(tempName.IndexOf(" ")+1);
            }
        }
        public string Email { get; set; }
        public Uri Blog { get; set; }
        public Uri Avatar { get; set; }
    }
}
