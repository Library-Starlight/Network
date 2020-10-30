using HttpListener.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HttpListener.Core.Controllers
{
    public class MbGeomagnetismController : ControllerBase
    {
        #region 测试
        
        [Route("HelloWorld")]
        public string HelloWorld()
            => "hello world!";

        [Route("Object")]
        public Apple Object(Apple apple)
            => apple;

        #endregion

        #region 消息推送

        [Route("device/status")]
        [HttpPost]
        public ActionResult Status([FromBody] DeviceStatus status)
            => OkMessage(status);

        [Route("device/info")]
        [HttpPost]
        public ActionResult Strstat([FromBody] DeviceInfo info)
            => OkMessage(info);

        private OkObjectResult OkMessage(object obj)
            => Ok(JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true }));

        #endregion
    }
}
