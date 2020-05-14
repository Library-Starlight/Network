using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients
{
    /// <summary>
    /// 车辆进出记录查询请求
    /// </summary>
    public class VehicleEntryRecordRequest : JhtCloudRequest
    {
        /// <summary>
        /// 停车场编号
        /// </summary>
        public string parkCode { get; set; }

        /// <summary>
        /// 车牌号码，不传carNo则查询该停车场所有车辆
        /// </summary>
        public string carNo { get; set; }

        /// <summary>
        /// 开始时间，格式：yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string beginTime { get; set; }

        /// <summary>
        /// 结束时间，格式：yyyy-MM-dd HH:mm:ss，与开始时间间隔不能大于30天
        /// </summary>
        public string endTime { get; set; }

        /// <summary>
        /// 查询条数，查询条数，最大100
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// 查询页码
        /// </summary>
        public int pageIndex { get; set; }
    }
}
