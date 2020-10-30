using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jhpj.ApiModel
{
    /// <summary>
    /// 请求模型
    /// </summary>
    public class JhpjApiModel
    {
        /// <summary>
        /// 应用程序Id
        /// </summary>
        [JsonProperty("app_id")]
        public string AppId { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        /// <summary>
        /// 加密数据
        /// </summary>
        [JsonProperty("data")]
        public string Data { get; set; }

        /// <summary>
        /// 签名数据
        /// </summary>
        [JsonProperty("sign")]
        public string Sign { get; set; }
    }
}
