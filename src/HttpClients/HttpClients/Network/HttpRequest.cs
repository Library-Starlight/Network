using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients
{
    public class HttpRequest
    {
        #region 公共方法

        /// <summary>
        /// 发送HttpPost请求，并获取应答
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="body">请求数据体</param>
        /// <param name="headers">请求头部</param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url, string body, IDictionary<string, string> headers = null)
        {
            // 创建请求
            var request = HttpWebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "post";

            // 添加头部
            if (headers != null)
                foreach (var header in headers)
                    request.Headers.Add(header.Key, header.Value);

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
                var json = await sr.ReadToEndAsync();
                return json;
            }
        }

        public static async Task<string> GetAsync(string url, IDictionary<string, string> param, IDictionary<string, string> headers = null)
        {
            // 构建完整的HttpGet请求Url
            url = AppendHttpGetParam(url, param);

            // 创建请求
            var request = HttpWebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "get";

            // 添加头部
            if (headers != null)
                foreach (var header in headers)
                    request.Headers.Add(header.Key, header.Value);

            // 接收应答
            var response = request.GetResponse();
            using (var stream = response.GetResponseStream())
            using (var sr = new StreamReader(stream))
            {
                var json = await sr.ReadToEndAsync();
                return json;
            }
        }

        #endregion

        #region 工具方法

        /// <summary>
        /// 在Url上添加请求参数
        /// </summary>
        /// <param name="url">基础Url</param>
        /// <param name="param">请求参数字典</param>
        /// <returns></returns>
        private static string AppendHttpGetParam(string url, IDictionary<string, string> param)
        {
            // 若无参数，则返回url
            if (param == null || param.Count <= 0)
                return url;

            var sb = new StringBuilder();
            sb.Append(url);
            var first = true;
            foreach (var kv in param)
            {
                // 对数据进行Url编码
                var encodedValue = WebUtility.UrlEncode(kv.Value);
                if (!first)
                {
                    sb.Append($"&{kv.Key}={encodedValue}");
                }
                else
                {
                    sb.Append($"?{kv.Key}={encodedValue}");
                    first = false;
                }
            }
            return sb.ToString();
        }

        #endregion
    }
}
