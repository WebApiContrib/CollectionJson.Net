using Autofac;

using Autofac.Integration.WebApi;
using FriendApi.Infrastructure;
using FriendApi.Models;

using Autofac.Builder;
using CollectionJson.Controllers;
using System.Web.Http;

namespace FriendApi
{
    public static class ServiceConfiguration
    {
        public static void Configure(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                "default", "{controller}/{id}",
                new { id = RouteParameter.Optional });

            var builder = new ContainerBuilder();
            builder.RegisterType<FakeFriendRepository>().As<IFriendRepository>();
            builder.RegisterType<FriendDocumentWriter>().As<ICollectionJsonDocumentWriter<Friend>>();
            builder.RegisterType<FriendDocumentReader>().As<ICollectionJsonDocumentReader<Friend>>();
            builder.RegisterApiControllers(typeof(ServiceConfiguration).Assembly);
            builder.RegisterHttpRequestMessage(config);

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            
            config.DependencyResolver = resolver;
            config.Formatters.Add(new NegotiatingCollectionJsonFormatter());
         
        }

    }
}
