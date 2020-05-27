using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpListener.Model
{
    public class DeviceData
    {
        /// <summary>
        /// 设备Id
        /// </summary>
        public string deviceId { get; set; }

        /// <summary>
        /// pm2.5值
        /// </summary>
        public float pm25 { get; set; }

        /// <summary>
        /// pm10
        /// </summary>
        public float pm10 { get; set; }

        /// <summary>
        /// 噪声
        /// </summary>
        public float noise { get; set; }
        
        /// <summary>
        /// 温度
        /// </summary>
        public float tem { get; set; }

        /// <summary>
        /// 湿度
        /// </summary>
        public float hum { get; set; }

        /// <summary>
        /// 风力
        /// </summary>
        public float wp { get; set; }

        /// <summary>
        /// 风速
        /// </summary>
        public float ws { get; set; }

        /// <summary>
        /// 风向
        /// </summary>
        public WindOrientation wd { get; set; }
        
        /// <summary>
        /// tsp
        /// </summary>
        public float tsp { get; set; }
        
        /// <summary>
        /// 大气压
        /// </summary>
        public int atm { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime datatime { get; set; }
    }
}
