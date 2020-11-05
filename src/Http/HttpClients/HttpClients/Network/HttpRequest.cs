using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Http
{
    public static class HttpRequest
    {
        #region 公共方法

        /// <summary>
        /// 根据Http协议下载文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        public static void DownloadImageFile(string url, string path)
        {
            var request = WebRequest.Create(url);
            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                var img = Image.FromStream(stream);
                img.Save(path);
            }
        }

        /// <summary>
        /// 发送Http Post请求，并获取应答
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="body">请求数据体</param>
        /// <param name="headers">请求头部</param>
        /// <returns></returns>
        public static Task<string> PostAsync(string url, string body = null, IDictionary<string, string> param = null, IDictionary<string, string> headers = null)
        {
            return RequestAsync(HttpMethod.Post, url, body, param, headers);
        }

        /// <summary>
        /// 发送Http Get请求，并获取应答
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <param name="param"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static Task<string> GetAsync(string url, IDictionary<string, string> param = null, IDictionary<string, string> headers = null)
        {
            return RequestAsync(HttpMethod.Get, url, null, param, headers);
        }

        /// <summary>
        /// 获取查询字符串
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string GetQueryString(IDictionary<string, string> parameters)
        {
            var sb = new StringBuilder();

            // 若无参数，则返回url
            if (parameters == null || parameters.Count <= 0)
                return string.Empty;

            var first = true;
            foreach (var kv in parameters)
            {
                // 对数据进行Url编码
                var encodedValue = WebUtility.UrlEncode(kv.Value);
                if (!first)
                {
                    sb.Append($"&{kv.Key}={encodedValue}");
                }
                else
                {
                    sb.Append($"{kv.Key}={encodedValue}");
                    first = false;
                }
            }

            return sb.ToString();
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

        #endregion
    }
}
