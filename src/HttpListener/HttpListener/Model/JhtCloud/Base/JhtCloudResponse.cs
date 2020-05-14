using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpListener
{
    /// <summary>
    /// 捷慧通云平台应答
    /// </summary>
    public class JhtCloudResponse
    { }

    /// <summary>
    /// 捷慧通云平台应答
    /// </summary>
    public class JhtCloudResponse<T>
        where T : JhtCloudResponse
    {
        /// <summary>
        /// 返回码，0：成功，1：未知错误，2：参数不正确
        /// </summary>
        public int resultCode { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 数据项列表
        /// </summary>
        public IEnumerable<JhtCloudResponseItem<T>> dataItems { get; set; }
    }
}
