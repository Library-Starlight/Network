using HttpListener.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace HttpListener.Controller
{
    public class MessageController : ApiController
    {
        [HttpPost]
        [Route("Message/HelloWorld")]
        public string HelloWorld()
        {
            return "Hello World!";
        }

        [HttpPost]
        [Route("Message/obj")]
        public string Obj(Data data)
        {
            return "OK";
        }


        [HttpPost]
        [Route("Device/Data")]
        public IHttpActionResult Data(DeviceData data)
        {
            if (data.deviceId != "0")
            {
                return NotFound();
            }


            return Ok();
        }
    }
}
