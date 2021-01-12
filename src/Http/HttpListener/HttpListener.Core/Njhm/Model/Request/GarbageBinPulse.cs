using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWNjhmGarbage.NET
{
    /// <summary>
    /// 垃圾桶心跳数据
    /// </summary>
    public class GarbageBinPulse
    {
        public string id { get; set; }
        public string cmd { get; set; }
        public string deviceID { get; set; }
        public string version { get; set; }
        [JsonConverter(typeof(NoDelimiterDateTimeConverter))]
        public DateTime time { get; set; }
        public ConverAngleData data { get; set; }
    }

    /// <summary>
    /// 垃圾盖倾斜角度数据
    /// </summary>
    public class ConverAngleData
    {
        public int rssi { get; set; }
        public int angle { get; set; }
        public int distance { get; set; }
        public int battary { get; set; }
    }
}
