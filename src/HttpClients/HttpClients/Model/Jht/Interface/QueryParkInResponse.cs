﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients
{
    public class QueryParkInResponse : JhtCloudResponse
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
        /// 入场时间，格式为：“yyyy-MM-dd HH:mm:ss”
        /// </summary>
        [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime inTime { get; set; }

        /// <summary>
        /// 入场事件名
        /// </summary>
        public string inEventType { get; set; }

        /// <summary>
        /// 入场设备名
        /// </summary>
        public string inEquip { get; set; }

        /// <summary>
        /// 入场操作员
        /// </summary>
        public string inOperator { get; set; }
    }
}
