using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpClients.Model
{
    public class DeviceData : DataBase
    {
        /// <summary>
        /// 通道数
        /// </summary>
        public byte ChannelCount { get; set; }

        /// <summary>
        /// 通道数据
        /// </summary>
        public Dictionary<string, ChannelData> Datas { get; set; }
    }
}
