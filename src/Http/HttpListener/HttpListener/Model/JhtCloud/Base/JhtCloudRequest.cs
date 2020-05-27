using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpListener
{
    /// <summary>
    /// 捷慧通云平台请求参数基类
    /// </summary>
    public class JhtCloudRequest
    {
    }

    /// <summary>
    /// 捷慧通云平台请求
    /// </summary>
    public class JhtCloudRequest<T>
        where T : JhtCloudRequest
    {
        /// <summary>
        /// 服务标识（请求类型名称的小写格式与该值匹配）
        /// </summary>
        public string serviceId => typeof(T).Name.ToLower();

        /// <summary>
        /// 请求类型，固定值传入：DATA
        /// </summary>
        public string requestType => "DATA";

        /// <summary>
        /// 请求数据体
        /// </summary>
        public T attributes { get; set; }
    }
}
