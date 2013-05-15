using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApiContrib.CollectionJson;
using WebApiContrib.Formatting.CollectionJson;
using WebApiContrib.Formatting.CollectionJson.Infrastructure;
using WebApiContrib.Formatting.CollectionJson.Models;

namespace CollectionJson.Controllers
{
    // This example introduces a slightly enhanced formatter that looks for a interface on the return object
    // to know if it can be serialized as Collection+json
    // This requires an extra interface and formatter.

    public class Friends3Controller : ApiController
    {
        private readonly IFriendRepository _friendRepository;

        public Friends3Controller(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public FriendsViewModel Get(int id)
        {
            var viewModel = new FriendsViewModel();
            viewModel.Friend = _friendRepository.Get(id);
            return viewModel;
        }
    }

    public class FriendsViewModel : ICollectionJsonWriter
    {

        public Friend Friend { get; set; }

        public ReadDocument Write()
        {
            var writer = new FriendDocumentWriter();
            var readDocument = writer.Write(new List<Friend>() { Friend });
            return readDocument;
        }
    }


    public interface ICollectionJsonWriter
    {
        ReadDocument Write();
    }

   
    public class NegotiatingCollectionJsonFormatter : CollectionJsonFormatter
    {

        public override bool CanWriteType(Type type)
        {
            return base.CanWriteType(type) || typeof(ICollectionJsonWriter).IsAssignableFrom(type);
        }
        public override System.Threading.Tasks.Task WriteToStreamAsync(Type type, object value, System.IO.Stream writeStream, System.Net.Http.HttpContent content, System.Net.TransportContext transportContext)
        {
            if (typeof(ICollectionJsonWriter).IsAssignableFrom(type))
            {
                var writer = (ICollectionJsonWriter)value;
                var readDocument = writer.Write();
                return base.WriteToStreamAsync(typeof(ReadDocument), readDocument, writeStream, content, transportContext);
            }
            else
            {
                return base.WriteToStreamAsync(type, value, writeStream, content, transportContext);
            }
        }
    }

}