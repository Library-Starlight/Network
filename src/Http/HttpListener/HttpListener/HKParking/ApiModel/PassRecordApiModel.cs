
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKParking.ApiModel
{
    /// <summary>
    /// 实时车辆通信记录
    /// </summary>
    public class PassRecordApiModel
    {
        /// <summary>
        /// 停车场编号
        /// </summary>
        public string parkCode { get; set; }
        /// <summary>
        /// 过车信息唯一标识
        /// </summary>
        public string uniqueNo { get; set; }
        /// <summary>
        /// 过车行驶方向
        /// </summary>
        public int direction { get; set; }
        /// <summary>
        /// 车牌号码 
        /// </summary>
        public string plateNo { get; set; }
        /// <summary>
        /// 卡号 
        /// </summary>
        public string cardNo { get; set; }
        /// <summary>
        /// 通行时间
        /// </summary>
        public DateTime passTime { get; set; }
        /// <summary>
        /// 车辆类型 
        /// </summary>
        public int vehType { get; set; }
        /// <summary>
        /// 车辆颜色
        /// </summary>
        public int vehColor { get; set; }
        /// <summary>
        /// 操作员账号 
        /// </summary>
        public string operatorName { get; set; }
        /// <summary>
        /// 放行的终端编号
        /// </summary>
        public string terminalNo { get; set; }
        /// <summary>
        /// 出入口名称
        /// </summary>
        public string gateName { get; set; }
        /// <summary>
        /// 放行车道名称
        /// </summary>
        public string laneName { get; set; }
        /// <summary>
        /// (可选)放行方式
        /// 0-牌识放行，1-刷卡放行，2-取卡放行，3-收卡放行，4-手动放行，5-异常放行， 6-禁止通行，7-平台放行， 8-遥控放行；
        /// </summary>
        public string passType { get; set; }
        /// <summary>
        /// (可选)通行车道号
        /// </summary>
        public string laneCode { get; set; }
        /// <summary>
        /// (可选)出场对应的入场时间
        /// </summary>
        public DateTime? inPassTime { get; set; }
        /// <summary>
        /// (可选)出场对应的入场车辆唯一编号
        /// </summary>
        public string inUniqueNo { get; set; }
        /// <summary>
        /// (可选)出场应付金额，单位:分
        /// </summary>
        public int? shouldPay { get; set; }
        /// <summary>
        /// (可选)出场实付金额，单位:分
        /// </summary>
        public int? actualPay { get; set; }
        /// <summary>
        /// 过车图片相对路径
        /// </summary>
        public string picFilePath { get; set; }
        /// <summary>
        /// 车牌图片相对路径
        /// </summary>
        public string picPlateFilePath { get; set; }
        /// <summary>
        /// 数据类型，0 实时数据 1历史数据
        /// </summary>
        public int dataType { get; set; }
        /// <summary>
        /// (可选)停车场区域总数
        /// </summary>
        public int totalRegion { get; set; }
        /// <summary>
        /// (可选)停车场区域信息
        /// </summary>
        public List<Parkinfo> parkInfo { get; set; }
        /// <summary>
        /// ?
        /// </summary>
        public int chargType { get; set; }
        /// <summary>
        /// ?
        /// </summary>
        public int parkingType { get; set; }
    }
}
