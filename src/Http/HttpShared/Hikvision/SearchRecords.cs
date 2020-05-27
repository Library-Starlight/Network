using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpShared.Hikvision
{
    /// <summary>
    /// 查询车辆进出记录
    /// </summary>
    public class SearchRecords
    {
        public string parkSyscode { get; set; }

        public string entranceSyscode { get; set; }

        public string plateNo { get; set; }

        public string cardNo { get; set; }

        public DateTime startTime { get; set; }

        public DateTime endTime { get; set; }

        public int vehicleOut { get; set; }

        public int vehicleType { get; set; }

        public int releaseResult { get; set; }

        public int releaseWay { get; set; }

        public int releaseReason { get; set; }

        public string carCategory { get; set; }

        public int pageNo { get; set; }

        public int pageSize { get; set; }

    }
}
