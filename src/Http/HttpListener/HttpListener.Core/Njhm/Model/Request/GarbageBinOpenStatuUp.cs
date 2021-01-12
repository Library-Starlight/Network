using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWNjhmGarbage.NET
{
    /// <summary>
    /// 垃圾桶上盖状态
    /// </summary>
    public class GarbageBinOpenStatuUp
    {
        public string id { get; set; }
        public string cmd { get; set; }
        public string deviceID { get; set; }
        public string version { get; set; }
        [JsonConverter(typeof(NoDelimiterDateTimeConverter))]
        public DateTime time { get; set; }
        public OpenStatuData data { get; set; }
    }

    public class OpenStatuData
    {
        public int rssi { get; set; }
        public int state { get; set; }
        public int angle { get; set; }
        public int battary { get; set; }
    }
}
