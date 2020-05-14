using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpListener
{
    /// <summary>
    /// 捷慧通云平台应答子项
    /// </summary>
    public class JhtCloudResponseItem<T>
        where T : JhtCloudResponse
    {
        /// <summary>
        /// 子对象ID
        /// </summary>
        public string objectId { get; set; }

        /// <summary>
        /// 子对象操作类型，固定值传入：READ
        /// </summary>
        public string operateType { get; set; }

        /// <summary>
        /// 数据体
        /// </summary>
        public T attributes { get; set; }
    }
}
