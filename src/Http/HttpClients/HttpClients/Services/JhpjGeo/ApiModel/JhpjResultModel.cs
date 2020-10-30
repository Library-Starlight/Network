using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Jhpj.ApiModel
{
    /// <summary>
    /// 响应模型
    /// </summary>
    public class JhpjResultModel
    {

        #region 公共属性

        /// <summary>
        /// 请求Id
        /// </summary>
        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        /// <summary>
        /// 应答码
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// 应答消息
        /// </summary>
        [JsonProperty("msg")]
        public string Msg { get; set; }

        #endregion

        #region 静态字段

        /// <summary>
        /// 请求Id
        /// </summary>
        private static int _requestId;

        #endregion

        #region 静态方法

        /// <summary>
        /// 请求Id
        /// </summary>
        /// <returns></returns>
        private static string GenerateRequestId()
        {
            // requestId尾部
            var id = Interlocked.Increment(ref _requestId);
            var requestIdTail = id % 1000000;
            // requestId
            var requestId = DateTime.Now.ToString("yyyyMMddHHmmssfff") + requestIdTail.ToString().PadLeft(6, '0');

            return requestId;
        }

        /// <summary>
        /// 创建成功模型
        /// </summary>
        /// <returns></returns>
        public static JhpjResultModel CreateSuccessModel()
            => new JhpjResultModel
            {
                RequestId = GenerateRequestId(),
                Code = "0",
                Msg = "调用成功",
            };

        /// <summary>
        /// 创建失败应答
        /// </summary>
        /// <returns></returns>
        public static JhpjResultModel CreateFailureModel()
            => CreateFailureModel("调用失败");

        /// <summary>
        /// 创建失败应答
        /// </summary>
        /// <param name="error">错误消息</param>
        /// <returns></returns>
        public static JhpjResultModel CreateFailureModel(string error)
            => new JhpjResultModel
            {
                RequestId = GenerateRequestId(),
                Code = "-1",
                Msg = error,
            };

        #endregion
    }
}
