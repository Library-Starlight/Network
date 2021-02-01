using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClients.Services.SwResolve.ApiModel
{
    public class StationResultModel
    {
        public StationModel[] Data { get; set; }
        public ApiCode Code { get; set; }
        public string Message { get; set; }
    }

    public class StationModel
    {
        /// <summary>
        /// 站点名
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        /// 站点类型（0：基准站，1：监测站）
        /// </summary>
        public int StationType { get; set; }

        /// <summary>
        /// 站点启用状态（0：禁用，1启用）
        /// </summary>
        public int Enable { get; set; }

        /// <summary>
        /// 设备机身号	
        /// </summary>
        public string DeviceSn { get; set; }

        /// <summary>
        /// 设备类型（0：Ms3x1，1：Ms3x2）
        /// </summary>
        public int DeviceType { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 坐标类型（0：空间直角坐标，1：大地坐标（经纬度），2：高斯平面坐标
        /// </summary>
        public int CoordinateType { get; set; }

        /// <summary>
        /// X坐标	
        /// </summary>
        public string X { get; set; }

        /// <summary>
        /// Y坐标	
        /// </summary>
        public string Y { get; set; }

        /// <summary>
        /// Z坐标	
        /// </summary>
        public string Z { get; set; }

        /// <summary>
        /// 是否为主站点
        /// </summary>
        public bool IsMainBase { get; set; }

        /// <summary>
        /// 站点Id
        /// </summary>
        public int Id { get; set; }
    }
}
