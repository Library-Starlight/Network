using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpListener
{
    /// <summary>
    /// 登录结果码
    /// </summary>
    public enum HycResultCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,

        /// <summary>
        /// 错误
        /// </summary>
        Error = 500,

        /// <summary>
        /// 用户未鉴权
        /// </summary>
        Unauthorized = 101, 

        /// <summary>
        /// 查询任务结果未鉴权
        /// </summary>
        SearchTaskResultUnauthorized = -102,

        /// <summary>
        /// 查询Web令牌未鉴权
        /// </summary>
        GetWebTokenUnauthorized = -103,
    }
}
