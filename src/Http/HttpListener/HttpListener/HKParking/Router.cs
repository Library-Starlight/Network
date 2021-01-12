using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKParking
{
    /// <summary>
    /// 路由
    /// </summary>
    public static class Router
    {
        /// <summary>
        /// 实时通信记录
        /// </summary>
        public const string PassRecord = "api/passRecord";

        /// <summary>
        /// 入场是否可以放行
        /// </summary>
        public const string CanIn = "api/canIn";

        /// <summary>
        /// 出场是否可以放行
        /// </summary>
        public const string CanOut = "api/canOut";
    }
}
