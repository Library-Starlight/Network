using System;

namespace StreetLED.Model.Response
{
    /// <summary>
    /// 设备信息
    /// </summary>
    public class DeviceDataModel
    {
        public string sn { get; set; }
        public int type { get; set; }
        public string name { get; set; }
        public bool isOnline { get; set; }
        public double lng { get; set; }
        public double lat { get; set; }
        public DateTime heartbeatTime { get; set; }
        public PlayStete state { get; set; }
        public DeviceDetails details { get; set; }
    }

    public class DeviceDetails
    {
        /// <summary>
        /// 亮度
        /// </summary>
        public Brightness brightness { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public int width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        public int height { get; set; }

        /// <summary>
        /// 分辨率
        /// </summary>
        public int rotation { get; set; }

        /// <summary>
        /// 0 单节目循环，1 顺序播放
        /// </summary>
        public PlayMode playMode { get; set; }
        /// <summary>
        /// 系统音量大小，取值 0 ~ 100
        /// </summary>
        public int volume { get; set; }
    }

    public class Brightness
    {
        public string mode { get; set; }
        public int @fixed { get; set; }
    }

    public enum PlayStete
    {
        播放中 = 0,
        停止播放 = -3,
        正在播放U盘内容 = -4,
        播放器崩溃 = -5,
        已关屏 = -6,
        license已失效 = -7,
        节目不匹配_USB = -8,
        节目不匹配_MTP = -9,
        同步播放状态 = -10,
        节目文件不存在 = -11,
        屏幕测试中 = -12,
        设备被锁定 = -13,

        未知 = -1001,
    }

    public enum PlayMode
    { 
        单节目循环 = 0,
        顺序播放 = 1,

        未知 = -1001,
    }
}
