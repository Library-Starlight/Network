using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKParking.ApiModel
{
    /// <summary>
    /// 车辆出场信息
    /// </summary>
    public class CanOutApiModel
    {
        /// <summary>
        /// 停车场编号
        /// </summary>
        public string parkCode { get; set; }
        /// <summary>
        /// 车牌号码
        /// </summary>
        public string plateNo { get; set; }
        /// <summary>
        /// 车辆通行时间 
        /// </summary>
        public string passTime { get; set; }
        /// <summary>
        /// 车辆类型
        /// </summary>
        public int vehType { get; set; }
        /// <summary>
        /// 通行车道号
        /// </summary>
        public string lanCode { get; set; }
        /// <summary>
        /// (可选)出入口名称
        /// </summary>
        public string gateName { get; set; }
        /// <summary>
        /// (可选)放行车道名称
        /// </summary>
        public string laneName { get; set; }
        /// <summary>
        /// (可选)过车信息唯一标识
        /// </summary>
        public string uniqueNo { get; set; }
        /// <summary>
        /// (可选)出场对应的入场车辆唯一编号
        /// </summary>
        public string inUniqueNo { get; set; }
        /// <summary>
        /// (可选)出场对应的入场时间
        /// </summary>
        public string inPassTime { get; set; }
    }
}
