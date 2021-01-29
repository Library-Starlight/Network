using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.Services.SwResolve.ApiModel
{
    public class ResolveDataResultModel
    {
        /// <summary>
        /// 解算数据列表
        /// </summary>
        public ResolveData[] Data { get; set; }

        /// <summary>
        /// 错误码，0：成功，1：失败，2：未授权，3：未登录
        /// </summary>
        public ApiCode Code { get; set; }

        /// <summary>
        /// 调用结果
        /// </summary>
        public string Message { get; set; }
    }

    public class ResolveData
    {
        /// <summary>
        /// 数据类型（0：静态解算，1：动态解算，2：RTK）
        /// </summary>
        public int DataType { get; set; }

        /// <summary>
        /// 数据列表	
        /// </summary>
        public ResolveDataItem[] DataList { get; set; }
    }

    public class ResolveDataItem
    {
        /// <summary>
        /// X坐标
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Z坐标
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// X偏移值
        /// </summary>
        public float DeltaX { get; set; }

        /// <summary>
        /// Y偏移值
        /// </summary>
        public float DeltaY { get; set; }

        /// <summary>
        /// Z偏移值
        /// </summary>
        public float DeltaZ { get; set; }

        /// <summary>
        /// 三维位移
        /// </summary>
        public float Displacement3D { get; set; }

        /// <summary>
        /// 方位角
        /// </summary>
        public float Azimuth { get; set; }

        /// <summary>
        /// 解算时间
        /// </summary>
        public string CollectTime { get; set; }

        /// <summary>
        /// 数据类型（0：静态解算，1：动态解算，2：RTK）
        /// </summary>
        public int DataType { get; set; }

        /// <summary>
        /// 解算时段
        /// </summary>
        public int CalculationTimeMinute { get; set; }
    }
}
