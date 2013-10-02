using WebApiContrib.CollectionJson;
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
using WebApiContrib.Formatting.CollectionJson.Server;

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

        protected override int Create(IWriteDocument writeDocument, HttpResponseMessage response)
        {
            var friend = Reader.Read(writeDocument);
            return repo.Add(friend);
        }

        protected override IReadDocument Read(HttpResponseMessage response)
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
