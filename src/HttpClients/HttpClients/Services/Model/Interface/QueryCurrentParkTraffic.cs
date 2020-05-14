using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients
{
    /// <summary>
    /// 车流量查询请求
    /// </summary>
    public class QueryCurrentParkTraffic : JhtCloudRequest
    {
        /// <summary>
        /// 停车场编号
        /// </summary>
        public string parkCode { get; set; }

        /// <summary>
        /// 查询日期
        /// </summary>
        [JsonConverter(typeof(StandardDateConverter))]
        public DateTime queryDate { get; set; }
    }
}
