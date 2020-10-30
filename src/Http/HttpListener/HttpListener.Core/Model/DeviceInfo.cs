using System;
using System.Collections.Generic;
using System.Text;

namespace HttpListener.Core.Model
{
    public class DeviceInfo
    {
        /// <summary>
        /// 地磁设备编号
        /// </summary>
        public string SN { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设备信息
        /// </summary>
        public TMoteInfo TMoteInfo { get; set; }
    }

    public class TMoteInfo
    {
        /// <summary>
        /// 设备电池电压，除以100后为真实值，如360表示3.6V
        /// </summary>
        public int Batt { get; set; }

        /// <summary>
        /// 温度，单位 摄氏度，电压会随着温度降低
        /// </summary>
        public int Temp { get; set; }

        /// <summary>
        /// 设备物联卡的卡号
        /// </summary>
        public string Sim { get; set; }

        /// <summary>
        /// 设备NB模组的编号
        /// </summary>
        public string Imei { get; set; }
    }
}
