using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpClients.Model
{
    /// <summary>
    /// 通道数据
    /// </summary>
    public class ChannelData
    {
        /// <summary>
        /// 通道主类型
        /// </summary>
        public ChannelType ChannelType { get; set; }

        /// <summary>
        /// 通道子类型
        /// </summary>
        public byte ChannelSubType { get; set; }

        /// <summary>
        /// 通道数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 获取通道主键
        /// </summary>
        /// <returns></returns>
        public string GetKey() => $"{(int)ChannelType}_{ChannelSubType}";
    }
}
