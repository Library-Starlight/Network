using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.Services.SwResolve.ApiModel
{
    public class AuthorizeApiModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserNameOrPhoneNumber { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 客户端类型
        /// </summary>
        public string ClientType { get; set; }
    }
}
