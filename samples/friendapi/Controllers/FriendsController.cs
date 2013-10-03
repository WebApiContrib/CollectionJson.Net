using FriendApi.Models;
using WebApiContrib.CollectionJson;
using WebApiContrib.Formatting.CollectionJson.Client;
using WebApiContrib.Formatting.CollectionJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using WebApiContrib.Formatting.CollectionJson.Server;

namespace FriendApi.Controllers
{
    public class FriendsController : CollectionJsonController<Friend>
    {
        private IFriendRepository repo;

        public FriendsController(IFriendRepository repo, ICollectionJsonDocumentWriter<Friend> writer, ICollectionJsonDocumentReader<Friend> reader)
            :base(writer, reader)
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
            var readDoc = Writer.Write(repo.GetAll());
            return readDoc;
        }

        protected override IReadDocument Read(int id, HttpResponseMessage response)
        {
            return Writer.Write(repo.Get(id));
        }
        
        public HttpResponseMessage Get(string name)
        {
            var friends = repo.GetAll().Where(f => f.FullName.IndexOf(name, StringComparison.OrdinalIgnoreCase) > -1);
            var readDocument = Writer.Write(friends);
            return readDocument.ToHttpResponseMessage();
        }

        protected override IReadDocument Update(int id, IWriteDocument writeDocument, HttpResponseMessage response)
        {
            var friend = Reader.Read(writeDocument);
            friend.Id = id;
            repo.Update(friend);
            return Writer.Write(friend);
        }

        protected override void Delete(int id, HttpResponseMessage response)
        {
            repo.Remove(id);
        }
    }
}
