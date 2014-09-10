using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using CollectionJson.Client;

namespace WebApiContrib.Formatting.CollectionJson.Tests
{
    public static class ApiControllerExtensions
    {
        public static void ConfigureForTesting(this ApiController controller, HttpRequestMessage request, string routeName = null, IHttpRoute route = null)
        {
            controller.Request = request;
            var config = new HttpConfiguration();
            controller.Configuration = config;

            var controllerTypeName = controller.GetType().Name;
            var controllerName = controllerTypeName.Substring(0, controllerTypeName.IndexOf("Controller")).ToLower();

            if (routeName != null && route != null)
                config.Routes.Add(routeName, route);
            else
                route = config.Routes.MapHttpRoute("DefaultApi", "{controller}/{id}", new { id = RouteParameter.Optional });

            controller.ControllerContext.ControllerDescriptor = new HttpControllerDescriptor(controller.Configuration, controllerName, controller.GetType());
   
            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", controllerName } }
            );

            controller.Configuration.Formatters.Add(new CollectionJsonFormatter());

        }
    }
}
