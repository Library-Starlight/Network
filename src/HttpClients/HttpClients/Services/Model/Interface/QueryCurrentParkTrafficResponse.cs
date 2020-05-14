using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients
{
    /// <summary>
    /// 车流量查询应答
    /// </summary>
    public class QueryCurrentParkTrafficResponse : JhtCloudResponse
    {
        /// <summary>
        /// 车流总量，当天截止当前车流总量
        /// </summary>
        public int totalTraffic { get; set; }

        /// <summary>
        /// 入场总数，当天截止当前入场流量
        /// </summary>
        public int parkinTraffic { get; set; }

        /// <summary>
        /// 出场总数，当天截止当前出场流量
        /// </summary>
        public int parkoutTraffic { get; set; }
    }
}
