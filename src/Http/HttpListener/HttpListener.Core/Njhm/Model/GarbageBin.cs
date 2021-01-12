using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWNjhmGarbage.NET
{
    /// <summary>
    /// 垃圾桶设备状态信息
    /// </summary>
    public class GarbageBin
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 状态变化时间
        /// </summary>
        public DateTime ReceiveTime { get; set; }

        /// <summary>
        /// 信号强度
        /// </summary>
        public int Rssi { get; set; }

        /// <summary>
        /// 距离，单位：厘米
        /// </summary>
        public int Distance { get; set; }

        /// <summary>
        /// 电量
        /// </summary>
        public int Battary { get; set; }

        /// <summary>
        /// 倾斜角，单位度，上盖的倾斜角度
        /// </summary>
        public int Angle { get; set; }

        /// <summary>
        /// 垃圾桶满溢状态，false：空，true：满
        /// </summary>
        public bool Full { get; set; }

        /// <summary>
        /// 垃圾桶盖子状态，false：关闭，true：打开
        /// </summary>
        public bool OpenState { get; set; }
    }
}
