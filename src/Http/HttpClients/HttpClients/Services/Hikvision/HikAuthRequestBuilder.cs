using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.Services.Hikvision
{
    /// <summary>
    /// 构造带认证的请求的帮助类
    /// </summary>
    public class HikAuthRequestBuilder
    {
        #region 公共方法

        /// <summary>
        /// 构建Http请求
        /// </summary>
        /// <param name="path">Uri相对路径</param>
        /// <param name="content">请求内容</param>
        /// <returns></returns>
        public HttpRequestMessage BuildRequest(string path, string content)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, path);

            // Accept


            var contentMessage = new StringContent(content);

            // Content-Type
            contentMessage.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            // Content-MD5
            contentMessage.Headers.ContentMD5 = new byte[16];

            return null;
        }

        #endregion
    }
}
