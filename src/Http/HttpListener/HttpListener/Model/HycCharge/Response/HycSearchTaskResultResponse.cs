using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpListener.Model.HycCharge.Response
{
    /// <summary>
    /// 用户执行结果
    /// </summary>
    /// <typeparam name="T">结果数据体，根据业务类型确定</typeparam>
    public class HycSearchTaskResultResponse
    {
        /// <summary>
        /// 错误码，1为成功，500为错误，101用户未鉴权
        /// </summary>
        public HycResultCode code { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 任务执行状态
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public HycOperationStatus operationStatus { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public HycTaskResultData data { get; set; }
    }


    public class HycTaskResultData
    {
        public HycTaskResultRow[] rows { get; set; }
        public int total { get; set; }
    }

    public class HycTaskResultRow
    {
        public string devImei { get; set; }
        public int devType { get; set; }
        public string stationName { get; set; }
        public string mchName { get; set; }
        public int logStatus { get; set; }
        public string addDate { get; set; }
        public object onenetDevId { get; set; }
        public string online { get; set; }
        public HycPlug[] plugs { get; set; }
    }

    public class HycPlug
    {
        public string plugNum { get; set; }
        public int plugStatus { get; set; }
    }
}