using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HttpServer.Controllers
{
    [Route("Hikvision")]
    [ApiController]
    public class HikvisionController : Controller
    {
        private ILogger _logger;

        public HikvisionController(ILogger<HikvisionController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public string Test(Authenticate auth)
        {
            //return $"{auth.AppKey}_{auth.AppSecret}";

            Console.WriteLine($"{auth.AppKey}_{auth.AppSecret}");



            Console.WriteLine($"Request Headers:");
            foreach (var header in ControllerContext.HttpContext.Request.Headers)
            {
                Console.WriteLine($"{header.Key}:{header.Value}");
            }

            return "OK";
        }
    }
}