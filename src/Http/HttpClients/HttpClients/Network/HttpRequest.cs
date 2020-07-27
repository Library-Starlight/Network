using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Http
{
    public static class HttpRequest
    {
        #region 公共方法

        /// <summary>
        /// 发送HttpPost请求，并获取应答
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="body">请求数据体</param>
        /// <param name="headers">请求头部</param>
        /// <returns></returns>
        public static Task<string> PostAsync(string url, string body = null, IDictionary<string, string> param = null, IDictionary<string, string> headers = null)
        {
            return RequestAsync(HttpMethod.Post, url, body, param, headers);
        }

        public static Task<string> GetAsync(string url, string body = null, IDictionary<string, string> param = null, IDictionary<string, string> headers = null)
        {
            return RequestAsync(HttpMethod.Get, url, body, param, headers);
        }

        #endregion

        #region 工具方法

        private static async Task<string> RequestAsync(HttpMethod method, string url, string body = null, IDictionary<string, string> param = null, IDictionary<string, string> headers = null)
        {
            // 构建完整的HttpGet请求Url
            url = AppendHttpGetParam(url, param);

            // 创建请求
            var request = HttpWebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = method.ToString();

            // 添加头部
            if (headers != null)
                foreach (var header in headers)
                    request.Headers.Add(header.Key, header.Value);

            // 发送请求
            if (!string.IsNullOrEmpty(body))
            {
                Stream stream = null;
                try
                {
                    stream = request.GetRequestStream();
                    using (var sw = new StreamWriter(stream))
                    {
                        stream = null;
                        await sw.WriteAsync(body);
                    }
                }
                finally
                {
                    if (stream != null)
                        stream.Dispose();
                }

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
