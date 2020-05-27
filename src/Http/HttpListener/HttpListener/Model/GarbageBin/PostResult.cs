using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpListener
{
    /// <summary>
    /// 请求结果
    /// </summary>
    public class PostResult
    {
        /// <summary>
        /// 请求结果代码，100表示成功，101表示失败
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 请求结果数据体
        /// </summary>
        public Body body { get; set; }
    }

    public class Body
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }
    }
}
