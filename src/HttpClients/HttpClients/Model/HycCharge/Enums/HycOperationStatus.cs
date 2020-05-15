using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients
{
    /// <summary>
    /// 查询操作状态
    /// </summary>
    public enum HycOperationStatus
    {
        /// <summary>
        /// 任务执行中
        /// </summary>
        running = 0,

        /// <summary>
        /// 任务执行成功
        /// </summary>
        success = 1,

        /// <summary>
        /// 任务执行失败
        /// </summary>
        failed = 2,
    }
}
