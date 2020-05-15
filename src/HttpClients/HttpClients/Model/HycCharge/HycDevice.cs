using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients
{
    public class HycDevice
    {
        /// <summary>
        /// 设备IMEI号
        /// </summary>
        public string IMEI { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public HycDeviceType DevType { get; set; }

        /// <summary>
        /// 电站名
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        /// 代理商名称
        /// </summary>
        public string McName { get; set; }

        /// <summary>
        /// 日志状态
        /// </summary>
        public int LogStatus { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddDate { get; set; }

        /// <summary>
        /// 设备在OneNet平台上的Id，NB设备特有
        /// </summary>
        public string OneNetDevId { get; set; }

        /// <summary>
        /// 设备在线状态，true：在线，false：失联
        /// </summary>
        public bool Online { get; set; }

        /// <summary>
        /// 0号插口状态
        /// </summary>
        public HycPlugStatus PlugStatus0 { get; set; }

        /// <summary>
        /// 1号插口状态
        /// </summary>
        public HycPlugStatus PlugStatus1 { get; set; }
    }
}
