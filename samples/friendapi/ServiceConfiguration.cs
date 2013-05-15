using Autofac;
using Autofac.Builder;
using WebApiContrib.CollectionJson;
using WebApiContrib.Formatting.CollectionJson.Controllers;
using WebApiContrib.Formatting.CollectionJson.Infrastructure;
using WebApiContrib.Formatting.CollectionJson.Models;
using WebApiContrib.Formatting.CollectionJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using WebApiContrib.IoC.Autofac;

namespace WebApiContrib.Formatting.CollectionJson
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
