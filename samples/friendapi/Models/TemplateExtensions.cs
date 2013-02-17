using WebApiContrib.Formatting.CollectionJson.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiContrib.Formatting.CollectionJson
{
    public static class TemplateExtensions
    {
        public static Friend ToFriend(this Template template, int id = 0) {
            var friend = new Friend();
            friend.FullName = template.Data.GetDataByName("name").Value;
            friend.Email = template.Data.GetDataByName("email").Value;
            friend.Blog = new Uri(template.Data.GetDataByName("blog").Value);
            friend.Avatar = new Uri(template.Data.GetDataByName("avatar").Value);
            friend.Id = id;
            return friend;
        }
    }
}
