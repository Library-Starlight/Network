using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpClients.Model
{
    public enum CommandType : byte
    {
        /// <summary>
        /// 定时数据
        /// </summary>
        TimingData = 0x12,
    }
}
