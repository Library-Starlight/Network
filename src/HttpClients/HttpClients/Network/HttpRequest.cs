using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients
{
    public class HttpRequest
    {
        public static async Task<string> GetAsync(string url, string body)
        {
            var request = HttpWebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "post";

            // 发送请求
            using (var stream = request.GetRequestStream())
            using (var sw = new StreamWriter(stream))
            {
                await sw.WriteAsync(body);
            }

            // 接收应答
            var response = request.GetResponse();
            using (var stream = response.GetResponseStream())
            using (var sr = new StreamReader(stream))
            {
                var result = await sr.ReadToEndAsync();
                return result;
            }
        }
    }
}
