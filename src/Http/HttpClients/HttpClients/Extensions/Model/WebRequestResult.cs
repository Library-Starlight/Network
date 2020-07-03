using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Http
{
    public class WebRequestResult<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Successful { get; set; }

        /// <summary>
        /// 错误内容
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Http应答状态码
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Api应答内容
        /// </summary>
        public T ServerResponse { get; set; }
    }
}
