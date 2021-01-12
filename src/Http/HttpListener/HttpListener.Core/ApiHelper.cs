using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace HttpListener.Core
{
    internal class ApiHelper
    {
        private readonly IWebHost _host;

        public ApiHelper(string hostUrl)
        {
            _host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(hostUrl)
                .UseStartup<Startup>()
                .Build();
        }

        public void Run()
        {
            _host.RunAsync();
        }

        public class Startup
        {
            public void ConfigureServices(IServiceCollection services)
            {
                //services.AddMvc();
                services.AddMvc(options => { options.EnableEndpointRouting = false; });
            }

            public void Configure(IApplicationBuilder app)
            {
                app.UseMvc(s => { s.MapRoute("default", "{controller}/{action}/{id?}", "test/index"); });
            }
        }
    }

    public class TestApi : ControllerBase
    {
        public static Func<string, bool> DelEvt;
        //public static Func<DevAddMsg, bool> AddEvt;

        [Route("test/func1")]
        public string Func1()
        {
            return "hello api";
        }

        //[Route("ganwei/device/createdevice")]
        //[HttpPost]
        //public AddRsp CreateDevice([FromBody] DevAddMsg msg)
        //{
        //    Logging.Single.Info("CreateDevice :" + JsonConvert.SerializeObject(msg));

        //    if ((AddEvt?.Invoke(msg)).GetValueOrDefault(false))
        //    {
        //        Logging.Single.Info("CreateDevice : return true");


        //        return new AddRsp
        //        {
        //            code = "0",
        //            message = "",
        //            deviceId = msg.deviceId
        //        };
        //    }

        //    Logging.Single.Info("CreateDevice : return false");

        //    return new AddRsp
        //    {
        //        code = "1",
        //        message = "添加失败"
        //    };
        //}

        [Route("ganwei/device/deleteDevice/{deviceId}")]
        [HttpDelete]
        public DelRsp DeleteDevice(string deviceId)
        {
            //Logging.Single.Info("DeleteDevice :" + deviceId);

            if ((DelEvt?.Invoke(deviceId)).GetValueOrDefault(false))
            {
                //Logging.Single.Info("DeleteDevice return true");

                return new DelRsp
                {
                    code = "0",
                    message = ""
                };
            }

            //Logging.Single.Info("DeleteDevice return false");

            return new DelRsp
            {
                code = "1",
                message = "添加失败"
            };
        }

        public class AddRsp
        {
            public string code { get; set; }
            public string message { get; set; }
            public string deviceId { get; set; }
        }

        public class DelRsp
        {
            public string code { get; set; }
            public string message { get; set; }
        }
    }
}