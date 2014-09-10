using FriendApi.Models;
using CollectionJson;
using System;

namespace FriendApi.Infrastructure
{
    public class FriendDocumentReader : ICollectionJsonDocumentReader<Friend>
    {
        public Friend Read(IWriteDocument document)
        {
            var template = document.Template;
            var friend = new Friend();
            friend.FullName = template.Data.GetDataByName("name").Value;
            friend.Email = template.Data.GetDataByName("email").Value;
            friend.Blog = new Uri(template.Data.GetDataByName("blog").Value);
            friend.Avatar = new Uri(template.Data.GetDataByName("avatar").Value);
            return friend;
        }
    }

}
