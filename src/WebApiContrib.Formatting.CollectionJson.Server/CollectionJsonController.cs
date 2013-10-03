using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebApiContrib.CollectionJson;
using WebApiContrib.Formatting.CollectionJson.Client;

namespace WebApiContrib.Formatting.CollectionJson.Server
{
    public abstract class CollectionJsonController<TData> : CollectionJsonController<TData, int>
    {
        protected CollectionJsonController(ICollectionJsonDocumentWriter<TData> writer, ICollectionJsonDocumentReader<TData> reader, string routeName = "DefaultApi") :
            base(writer, reader, routeName)
        {
        }
    }

    public abstract class CollectionJsonController<TData, TId> : ApiController
    {
        private string routeName;
        protected ICollectionJsonDocumentWriter<TData> Writer { get; set; }
        protected ICollectionJsonDocumentReader<TData> Reader { get; set; }

        public CollectionJsonController(ICollectionJsonDocumentWriter<TData> writer, ICollectionJsonDocumentReader<TData> reader, string routeName = "DefaultApi")
        {
            this.routeName = routeName;
            this.Writer = writer;
            this.Reader = reader;
        }

        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            controllerContext.Configuration.Formatters.Insert(0,new CollectionJsonFormatter());
            base.Initialize(controllerContext);
        }

        private string ControllerName
        {
            get { return this.ControllerContext.ControllerDescriptor.ControllerName; }
        }

        public HttpResponseMessage Get()
        {
            var response = new HttpResponseMessage();
            var readDocument = this.Read(response);
            response.Content = readDocument.ToObjectContent();
            return response;
        }

        public HttpResponseMessage Get(TId id)
        {
            var response = new HttpResponseMessage();
            var readDocument = this.Read(id, response);
            response.Content = readDocument.ToObjectContent();
            return response;
        }

        public HttpResponseMessage Post(IWriteDocument document)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Created);
            var id = Create(document, response);
            response.Headers.Location = new Uri(Url.Link(this.routeName, new { controller = this.ControllerName, id = id }));
            return response;
        }

        public HttpResponseMessage Put(TId id, IWriteDocument document)
        {
            var response = new HttpResponseMessage();
            var readDocument = this.Update(id, document, response);
            response.Content = readDocument.ToObjectContent();
            return response;
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Remove(TId id)
        {
            var response = new HttpResponseMessage();
            Delete(id, response);
            return response;
        }

        protected virtual TId Create(IWriteDocument document, HttpResponseMessage response)
        {
            throw new HttpResponseException(System.Net.HttpStatusCode.NotImplemented);
        }

        protected virtual IReadDocument Read(TId id, HttpResponseMessage response)
        {
            throw new HttpResponseException(System.Net.HttpStatusCode.NotImplemented);
        }

        protected virtual IReadDocument Read(HttpResponseMessage response)
        {
            throw new HttpResponseException(System.Net.HttpStatusCode.NotImplemented);
        }

        protected virtual IReadDocument Update(TId id, IWriteDocument writeDocument, HttpResponseMessage response)
        {
            throw new HttpResponseException(System.Net.HttpStatusCode.NotImplemented);
        }

        protected virtual void Delete(TId id, HttpResponseMessage response)
        {
            throw new HttpResponseException(System.Net.HttpStatusCode.NotImplemented);
        }
    }
}
