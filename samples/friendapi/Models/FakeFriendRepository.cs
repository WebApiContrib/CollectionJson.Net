using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiContrib.Formatting.CollectionJson.Models
{
    public class FakeFriendRepository : IFriendRepository
    {
        private static List<Friend> friends = new List<Friend>();
        private static int id = 1;

        static FakeFriendRepository()
        {
            friends.Add(new Friend { Id = id++, FullName = "J. Doe", Email = "jdoe@example.org", Blog = new Uri("http://examples.org/blogs/jdoe"), Avatar = new Uri("http://examples.org/images/jode") });
            friends.Add(new Friend { Id = id++, FullName = "M. Smith", Email = "msmith@example.org", Blog = new Uri("http://examples.org/blogs/msmith"), Avatar = new Uri("http://examples.org/images/msmith") });
            friends.Add(new Friend { Id = id++, FullName = "R. Williams", Email = "rwilliams@example.org", Blog = new Uri("http://examples.org/blogs/rwilliams"), Avatar = new Uri("http://examples.org/images/rwilliams") });
        }

        public FakeFriendRepository()
        {
        }

        public IEnumerable<Friend> GetAll()
        {
            return friends;
        }

        public Friend Get(int id)
        {
            return friends.FirstOrDefault(f => f.Id == id);
        }

        public int Add(Friend friend)
        {
            friend.Id = id++;
            friends.Add(friend);
            return friend.Id;
        }

        public void Remove(int id)
        {
            var friend = Get(id);
            friends.Remove(friend);
        }

        public void Update(Friend friend)
        {
            var existingFriend = Get(friend.Id);
            existingFriend.Blog = friend.Blog;
            existingFriend.Avatar = friend.Avatar;
            existingFriend.Email = friend.Email;
            existingFriend.FullName = friend.FullName;
        }
    }
}
