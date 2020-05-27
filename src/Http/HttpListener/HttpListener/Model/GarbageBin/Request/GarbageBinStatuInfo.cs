using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpListener
{
    /// <summary>
    /// 垃圾桶状态
    /// </summary>
    public class GarbageBinStatuInfo
    {
        /// <summary>
        /// 随机数，UUID的随机数
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 指令编码，sendGarbagebinStatuInfo
        /// </summary>
        public string cmd { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string deviceID { get; set; }

        /// <summary>
        /// 接口版本信息，当前是1.0
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// 车位状态变化时间
        /// </summary>
        [JsonConverter(typeof(NoDelimiterDateTimeConverter))]
        public DateTime time { get; set; }

        /// <summary>
        /// 数据体
        /// </summary>
        public BinStatusData data { get; set; }
    }

    /// <summary>
    /// 垃圾桶状态数据
    /// </summary>
    public class BinStatusData
    {
        /// <summary>
        /// 信号强度，终端设备接收信号强度
        /// </summary>
        public int rssi { get; set; }

        /// <summary>
        /// 垃圾桶状态，0-空，1-满
        /// </summary>
        public int full { get; set; }

        /// <summary>
        /// 距离，单位厘米
        /// </summary>
        public int distance { get; set; }

        /// <summary>
        /// 电量，单位是%（如 90%）
        /// </summary>
        public int battary { get; set; }
    }

}
