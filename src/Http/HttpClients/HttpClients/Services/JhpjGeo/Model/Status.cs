using System;
using System.Collections.Generic;
using System.Text;

namespace Jhpj.Model
{
    public class Status
    {
        /// <summary>
        /// 通信包
        /// </summary>
        public string pid { get; set; }

        /// <summary>
        /// 唯一序列号
        /// </summary>
        public int uuid { get; set; }

        /// <summary>
        /// 车位号
        /// </summary>
        public string parkno { get; set; }

        /// <summary>
        /// 地磁序列号
        /// </summary>
        public string serial { get; set; }

        /// <summary>
        /// 车位状态
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 基站ID
        /// </summary>
        public string cellid { get; set; }

        /// <summary>
        /// 信号质量
        /// </summary>
        public string rsrp { get; set; }

        /// <summary>
        /// 发射功率
        /// </summary>
        public int txpow { get; set; }

        /// <summary>
        /// 信噪比
        /// </summary>
        public int sinr { get; set; }

        /// <summary>
        /// 物理小区标识
        /// </summary>
        public int pci { get; set; }

        /// <summary>
        /// 覆盖等级
        /// </summary>
        public int ec1 { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public string event_time { get; set; }
    }
}
