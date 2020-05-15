using HttpListener.Model;
using HttpListener.Model.HycCharge.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace HttpListener
{
    public class HycChargeController : ApiController
    {
        [HttpGet]
        [Route("HycCharge")]
        public string Test()
        {
            return "Hello World!";
        }

        [HttpPost]
        [Route("xyyc_sdk_api/rest/user/agentLogin")]
        public async Task<HycLoginResponse> AgentLogin(HycLogin request)
        {
            return await SendResponse<HycLogin, HycLoginResponse>(request);
        }

        [HttpPost]
        [Route("xyyc_sdk_api/rest/device/getDevices")]
        public async Task<HycGetDevicesResponse> GetDevices(HycGetDevices request)
        {

            return await SendResponse<HycGetDevices, HycGetDevicesResponse>(request);
        }

        [HttpGet]
        [Route("xyyc_sdk_api/rest/query/result/")]
        public async Task<HycSearchTaskResultResponse> QueryResult(string operationTaskId)
        {
            return await SendResponse<string, HycSearchTaskResultResponse>(operationTaskId);
        }


        #region 私有方法

        private async Task<TResponse> SendResponse<TRequest, TResponse>(TRequest request)
        {
            PrintRequestLog(request);
            using (var fs = new FileStream($"Texts/{typeof(TResponse).Name}.json", FileMode.Open, FileAccess.Read))
            using (var sr = new StreamReader(fs, Encoding.GetEncoding("GBK")))
            {
                var json = await sr.ReadToEndAsync();
                var response = JsonConvert.DeserializeObject<TResponse>(json);
                PrintResponseLog(response);
                return response;
            }
        }

        private void PrintRequestLog(object obj)
        {
            Console.WriteLine($"接收请求：{JsonConvert.SerializeObject(obj, Formatting.None)}");
            if (Request.Headers.Contains("Token"))
                Console.WriteLine($"  Token: {Request.Headers.GetValues("Token").FirstOrDefault()}");
        }
        private void PrintResponseLog(object obj)
        {
            Console.WriteLine($"发送应答：{JsonConvert.SerializeObject(obj, Formatting.None)}");
        }

        #endregion
    }
}
