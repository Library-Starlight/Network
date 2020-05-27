using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpListener
{
    /// <summary>
    /// 请求命令名称
    /// </summary>
    public enum RequestCmd
    {
        sendGarbagebinStatuInfo = 0,
        sendPulse = 1,
        sendGarbagebinOpenStatuUp = 2,
    }
}
