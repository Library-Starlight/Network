using System;
using System.Collections.Generic;
using System.Text;

namespace Jhpj.Model
{
    public class Info
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
        /// 版本号
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// 电池（电压）
        /// </summary>
        public string bettery { get; set; }

        /// <summary>
        /// 剩余电量
        /// </summary>
        public int voltameter { get; set; }

        /// <summary>
        /// 电量报警
        /// </summary>
        public string batterylow { get; set; }
        
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
        /// 信号质量
        /// </summary>
        public string quality { get; set; }

        /// <summary>
        /// SIM卡号
        /// </summary>
        public string sim { get; set; }

        /// <summary>
        /// SIM卡有效期
        /// </summary>
        public string sim_begin { get; set; }

        /// <summary>
        /// SIM卡有效期
        /// </summary>
        public string sim_end { get; set; }

        /// <summary>
        /// SIM卡有效期剩余时间
        /// </summary>
        public string sim_remain { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public string event_time { get; set; }
    }
}
