using WebApiContrib.Formatting.CollectionJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.SelfHost;

namespace WebApiContrib.Formatting.CollectionJson
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration(new Uri("http://localhost:9200"));
 
            ServiceConfiguration.Configure(config);

            var host = new HttpSelfHostServer(config);

            host.OpenAsync().Wait();
            Console.WriteLine("Collection hosted at http://localhost:9200/friends");
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();

            host.CloseAsync().Wait();
        }
    }
}
