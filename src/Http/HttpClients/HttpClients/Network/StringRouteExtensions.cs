using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace System.Net.Http
{
    public static class StringRouteExtensions
    {
        #region 扩展方法

        /// <summary>
        /// 添加查询字符串
        /// </summary>
        /// <param name="url">基础Url</param>
        /// <param name="param">请求参数字典</param>
        /// <returns></returns>
        public static string AppendQueryString(this string url, IDictionary<string, string> param)
        {
            if (param == null || param.Count <= 0)
                return url;

            return $"{url}?{GetQueryString(param)}";
        }

        /// <summary>
        /// 附加路径
        /// </summary>
        /// <param name="baseUrl">服务地址</param>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static string AppendRoute(this string baseUrl, string path)
        {
            path = NormalizePath(path);
            return baseUrl.EndsWith("/") ? $"{baseUrl}{path}" : $"{baseUrl}/{path}";
        }

        /// <summary>
        /// 转化为查询字符串
        /// </summary>
        /// <param name="parameters">参数列表</param>
        /// <returns></returns>
        public static string ToQueryString(this IDictionary<string, string> parameters)
          => parameters.Aggregate(string.Empty, (prev, cur) => $"{prev}&{cur.Key}={HttpUtility.UrlEncode(cur.Value)}").TrimStart('&');

        #endregion

        #region 公共方法

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

        /// <summary>
        /// Normalizing a path based on http uri
        /// </summary>
        /// <param name="path">The path to normalize</param>
        /// <returns></returns>
        private static string NormalizePath(string path)
        {
            // Replace any \ with /
            return path?.Replace('/', '\\').Trim();
        }

        #endregion
    }
}
