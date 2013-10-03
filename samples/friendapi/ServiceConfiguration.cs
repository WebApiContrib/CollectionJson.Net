using Autofac;
using FriendApi.Infrastructure;
using FriendApi.Models;
using WebApiContrib.CollectionJson;
using System.Web.Http;
using WebApiContrib.IoC.Autofac;

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

            var container = builder.Build();
            var resolver = new WebApiContrib.IoC.Autofac.AutofacResolver(container);
            
            config.DependencyResolver = resolver;
        }

    }
}
