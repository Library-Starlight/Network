using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients
{
    public class HycGetDevices
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int currentPage { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public HycDeviceType devType { get; set; }

        /// <summary>
        /// 条目数
        /// </summary>
        public int itemsPerPage { get; set; }
    }
}
