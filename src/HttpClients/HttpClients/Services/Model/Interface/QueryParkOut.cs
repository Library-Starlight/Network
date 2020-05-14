using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients
{
    /// <summary>
    /// 车辆出场信息查询请求
    /// </summary>
    public class QueryParkOut : JhtCloudRequest
    {
        /// <summary>
        /// 停车场编号
        /// </summary>
        public string parkCode { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string carNo { get; set; }

        /// <summary>
        /// 查询开始时间，格式：“yyyy-MM-dd HH:mm:ss”
        /// </summary>
        [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime beginDate { get; set; }

        /// <summary>
        /// 查询结束时间，格式：“yyyy-MM-dd HH:mm:ss” 注：起止时间间隔不超过一个月
        /// </summary>
        [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime endDate { get; set; }

        /// <summary>
        /// 查询页码
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// 查询条数（最大100条）
        /// </summary>
        public int pageIndex { get; set; }
    }
}
