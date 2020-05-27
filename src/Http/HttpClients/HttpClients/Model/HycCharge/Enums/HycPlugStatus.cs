using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients
{
    /// <summary>
    /// 插口状态
    /// </summary>
    public enum HycPlugStatus
    {
        失联 = -1,
        空闲 = 0,
        充电中 = 1,
        故障 = 2,
    }
}
