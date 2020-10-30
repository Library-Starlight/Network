using System;
using System.Collections.Generic;
using System.Text;

namespace HttpListener.Core.Model
{
    public class DeviceStatus
    {
        /// <summary>
        /// 地磁设备编号
        /// </summary>
        public string SN { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public TMoteStatus TMoteStatus { get; set; }

        /// <summary>
        /// 车位编号
        /// </summary>
        public string BerthCode { get; set; }
    }

    public class TMoteStatus
    {
        /// <summary>
        /// 状态
        /// 0：状态更新为空闲（车辆驶出）
        /// 1：状态更新为占用（车辆驶入）
        /// 2：心跳维持为空闲（无车心跳）
        /// 3：心跳维持为占用（有车心跳）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 设备状态变化计数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 车位状态变化的时间
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// 设备接收的信号强度指示
        /// </summary>
        public int Rssi { get; set; }

        /// <summary>
        /// 设备的信噪比
        /// </summary>
        public int Snr { get; set; }
    }
}
