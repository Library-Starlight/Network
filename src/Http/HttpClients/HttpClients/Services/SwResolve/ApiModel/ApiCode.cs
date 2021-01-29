using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.Services.SwResolve.ApiModel
{
    /// <summary>
    /// 错误码，0：成功，1：失败，2：未授权，3：未登录
    /// </summary>
    public enum ApiCode
    {
        Success = 0,
        Failure = 1,
        Unauthorized = 2,
        Unlogined = 3,
    }
}
