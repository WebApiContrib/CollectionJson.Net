using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace WebApiContrib.Formatting.CollectionJson
{
    public abstract class CollectionJsonController<TData> : CollectionJsonController<TData,int>
    {
        protected CollectionJsonController(ICollectionJsonDocumentWriter<TData> writer, ICollectionJsonDocumentReader<TData> reader, string routeName = "DefaultApi") :
            base(writer, reader, routeName)
        {
        }
    }

    public abstract class CollectionJsonController<TData,TId> : ApiController 
    {
        private CollectionJsonFormatter formatter = new CollectionJsonFormatter();
        private string routeName;
        private ICollectionJsonDocumentWriter<TData> writer;
        private ICollectionJsonDocumentReader<TData> reader;

        public CollectionJsonController(ICollectionJsonDocumentWriter<TData> writer, ICollectionJsonDocumentReader<TData> reader, string routeName = "DefaultApi")
        {
            this.routeName = routeName;
            this.writer = writer;
            this.reader = reader;
        }

        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            controllerContext.Configuration.Formatters.Add(formatter);
            base.Initialize(controllerContext);
        }

       
        private string ControllerName
        {
            get { return this.ControllerContext.ControllerDescriptor.ControllerName; }
        }

        private ObjectContent GetDocumentContent(ReadDocument document)
        {
            return new ObjectContent<ReadDocument>(document, formatter, "application/vnd.collection+json");
        }

        public HttpResponseMessage Get()
        {
            var response = new HttpResponseMessage();
            var data = this.Read(response);
            response.Content = GetDocumentContent(writer.Write(data));
            return response;
        }

        public HttpResponseMessage Get(TId id)
        {
            var response = new HttpResponseMessage();
            var data = this.Read(id, response);
            response.Content = GetDocumentContent(writer.Write(new[]{data}));
            return response;
        }

        public HttpResponseMessage Post(WriteDocument document)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Created);
            var id = Create(reader.Read(document), response);
            response.Headers.Location = new Uri(Url.Link(this.routeName, new { controller = this.ControllerName, id = id }));
            return response;
        }

        public HttpResponseMessage Put(TId id, WriteDocument document)
        {
            var response = new HttpResponseMessage();
            var data = this.Update(id, reader.Read(document), response);
            response.Content = GetDocumentContent(writer.Write(new TData[]{data}));
            return response;
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Remove(TId id)
        {
            var response = new HttpResponseMessage();
            Delete(id, response);
            return response;
        }

        protected virtual int Create(TData data, HttpResponseMessage response)
        {
            throw new HttpResponseException(System.Net.HttpStatusCode.NotImplemented);
        }

        protected virtual TData Read(TId id, HttpResponseMessage response)
        {
            throw new HttpResponseException(System.Net.HttpStatusCode.NotImplemented);
        }

        protected virtual IEnumerable<TData> Read(HttpResponseMessage response)
        {
            throw new HttpResponseException(System.Net.HttpStatusCode.NotImplemented);
        }

        protected virtual TData Update(TId id, TData data, HttpResponseMessage response)
        {
            throw new HttpResponseException(System.Net.HttpStatusCode.NotImplemented);
        }

        protected virtual void Delete(TId id, HttpResponseMessage response)
        {
            throw new HttpResponseException(System.Net.HttpStatusCode.NotImplemented);
        }
    }
}
