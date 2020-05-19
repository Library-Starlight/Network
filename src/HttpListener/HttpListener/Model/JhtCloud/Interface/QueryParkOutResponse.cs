using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpListener
{
    /// <summary>
    /// 车辆出场信息查询应答
    /// </summary>
    public class QueryParkOutResponse : JhtCloudResponse
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
        /// 车牌号
        /// </summary>
        public string carNo { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string cardNo { get; set; }

        /// <summary>
        /// 卡类型
        /// </summary>
        public string cardType { get; set; }

        /// <summary>
        /// 出场时间，格式为：“yyyy-MM-dd HH:mm:ss”
        /// </summary>
        [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime outTime { get; set; }

        /// <summary>
        /// 出场事件名
        /// </summary>
        public string outEventType { get; set; }

        /// <summary>
        /// 出场设备名
        /// </summary>
        public string outEquip { get; set; }

        /// <summary>
        /// 出场操作员
        /// </summary>
        public string outOperator { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string payTypeName { get; set; }

        /// <summary>
        /// 应缴金额
        /// </summary>
        public float ysMoney { get; set; }

        /// <summary>
        /// 优惠打折金额
        /// </summary>
        public float yhMoney { get; set; }

        /// <summary>
        /// 最高收费回滚减免金额
        /// </summary>
        public float hgMoney { get; set; }

        /// <summary>
        /// 实缴金额
        /// </summary>
        public float ssMoney { get; set; }

        /// <summary>
        /// 停车时长
        /// </summary>
        public int parkingTime { get; set; }

        /// <summary>
        /// 车辆出场图片url的id集合
        /// </summary>
        public string outPhotoUrlIds { get; set; }

        /// <summary>
        /// 入场时间
        /// </summary>
        [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime inTime { get; set; }

        /// <summary>
        /// 入场设备名
        /// </summary>
        public string inEquip { get; set; }
    }
}
