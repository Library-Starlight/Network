using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ZjgApi
{
    public class ZjgWeatherAccessTokenApi
    {
        /// <summary>
        /// 获取接口令牌
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetTokenAsync(string baseAddr, string appid, string secret)
        {
            var timestamp = DateTime.Now.ToUtcSecondInt64();
            var signature = GetSignature(appid, secret, timestamp);

            var parameters = new Dictionary<string, string>
            {
                { "appid", appid },
                { "timestamp", timestamp.ToString() },
                { "signature", signature },
            };

            var url = $"{baseAddr}WeatherService/Token.ashx";
            var result = await JsonHttpRequest.GetFromJsonAsync<TokenApiModel>(url, parameters);

            return result == null || result.Code != 0 ? string.Empty : result.Message;
        }

        /// <summary>
        /// 获取接口签名参数
        /// </summary>
        /// <param name="appid">应用程序Id</param>
        /// <param name="secret">应用程序密码</param>
        /// <param name="timestamp">时间戳</param>
        /// <returns></returns>
        private static string GetSignature(string appid, string secret, long timestamp)
        {
            // 拼接Http Get参数
            var parameters = new Dictionary<string, string>
            {
                { "appid", appid },
                { "secret", secret },
                { "timestamp", timestamp.ToString() },
            };
            // 获取查询字符串
            var query = HttpRequest.GetQueryString(parameters);

            // 生成签名
            var signature = MD5Encrypt.Encrypt(query).ToLower();

            return signature;
        }
    }
}
