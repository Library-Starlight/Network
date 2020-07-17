using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using System.Web.Http;
using HttpListener.Http;
using System.Net;

namespace HttpListener
{
    class Program
    {
        static void Main(string[] args)
        {
            StartHttpControllerListener();

            Console.WriteLine("启动成功");
            Console.ReadLine();
        }

        private static void StartHttpListenerNow()
        {
            try
            {
                var port = 11009;
                var ipEndPoint = new IPEndPoint(IPAddress.Loopback, port);
                new GarbageBinHttpServer(ipEndPoint).StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void StartHttpListenerNew()
        {
            _ = new Listener().Listen(10095);
        }

        private static void StartHttpControllerListener()
        {
            WebApp.Start("http://+:10095/", builder =>
            {
                var config = new HttpConfiguration();
                config.MapHttpAttributeRoutes();
                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{coutroller}/{action}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                    );
                builder.UseWebApi(config);
            });
        }
    }
}
