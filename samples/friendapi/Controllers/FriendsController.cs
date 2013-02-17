using WebApiContrib.Formatting.CollectionJson.Infrastructure;
using WebApiContrib.Formatting.CollectionJson.Models;
using WebApiContrib.Formatting.CollectionJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Net.Http;
using System.Net;

namespace WebApiContrib.Formatting.CollectionJson.Controllers
{
    public class FriendsController : CollectionJsonController<Friend>
    {
        private IFriendRepository repo;

        public FriendsController(IFriendRepository repo, ICollectionJsonDocumentWriter<Friend> builder, ICollectionJsonDocumentReader<Friend> transformer)
            :base(builder, transformer)
        {
            this.repo = repo;
        }

        protected override int Create(Friend friend, HttpResponseMessage response)
        {
            return repo.Add(friend);
        }

        protected override IEnumerable<Friend> Read(HttpResponseMessage response)
        {
            return repo.GetAll();
        }

        protected override Friend Read(int id, HttpResponseMessage response)
        {
            return repo.Get(id);
        }

        protected override Friend Update(int id, Friend friend, HttpResponseMessage response)
        {
            friend.Id = id;
            repo.Update(friend);
            return friend;
        }

        protected override void Delete(int id, HttpResponseMessage response)
        {
            repo.Remove(id);
        }
    }
}
