using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients
{
    /// <summary>
    /// 空车位查询应答
    /// </summary>
    public class QueryParkSpaceResponse : JhtCloudResponse
    {
        /// <summary>
        /// 停车场编号
        /// </summary>
        public string parkCode { get; set; }

        /// <summary>
        /// 停车场名称
        /// </summary>
        public string parkName { get; set; }

        /// <summary>
        /// 总车位数
        /// </summary>
        public int totalSpace { get; set; }

        /// <summary>
        /// 剩余车位数
        /// </summary>
        public int restSpace { get; set; }
    }
}
