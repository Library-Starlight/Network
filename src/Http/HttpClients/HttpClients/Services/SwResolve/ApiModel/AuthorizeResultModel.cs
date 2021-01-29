using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.Services.SwResolve.ApiModel
{
    public class AuthorizeResultModel
    {
        /// <summary>
        /// 授权数据
        /// </summary>
        public AuthorizeData Data { get; set; }

        /// <summary>
        /// 错误码，0：成功，1：失败，2：未授权，3：未登录
        /// </summary>
        public ApiCode Code { get; set; }

        /// <summary>
        /// 调用结果
        /// </summary>
        public string Message { get; set; }
    }

    public class AuthorizeData
    {
        /// <summary>
        /// 访问令牌
        /// </summary>
        public string AccessToken { get; set; }

        public string EncryptedAccessToken { get; set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        public int ExpireInSeconds { get; set; }
    }
}
