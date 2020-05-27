using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpListener
{
    public class HycLoginResponse
    {
        /// <summary>
        /// 错误码，1为成功，500为错误
        /// </summary>
        public HycResultCode code { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        public HycLoginResult result { get; set; }
    }

    public class HycLoginResult
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 用户令牌，后续需设置到请求头里
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// 登陆人的身份
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int id { get; set; }
    }
}
