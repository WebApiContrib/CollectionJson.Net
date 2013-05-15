using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using WebApiContrib.CollectionJson;
using WebApiContrib.Formatting.CollectionJson;
using WebApiContrib.Formatting.CollectionJson.Infrastructure;
using WebApiContrib.Formatting.CollectionJson.Models;

namespace CollectionJson.Controllers
{

    // Return Collection+json documents using the ReadDocument class and a standard ApiController
    // This works out of the box.  Just need to add CollectionJsonFormatter to global Formatters collection
    public class Friends2Controller : ApiController
    {
        private readonly IFriendRepository _friendRepository;

        public Friends2Controller(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public ReadDocument Get(int id)
        {
            var writer = new FriendDocumentWriter();
            var readDocument = writer.Write(new List<Friend>() {_friendRepository.Get(id)});
            return readDocument;
        }
    }


}
