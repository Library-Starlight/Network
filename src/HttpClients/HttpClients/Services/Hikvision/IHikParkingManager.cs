using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.Services.Hikvision
{
    /// <summary>
    /// 停车场管理系统接口
    /// </summary>
    public interface IHikParkingManager
    {
        /// <summary>
        /// 获取剩余车位数
        /// </summary>
        /// <returns></returns>
        Task<object> GetRemainSpacesAsync();
        
        /// <summary>
        /// 获取停车统计，即车辆出入记录
        /// </summary>
        /// <param name="beginTime">起始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        Task<object> GetParkStatAsync(DateTime beginTime, DateTime endTime);

        /// <summary>
        /// 获取图片链接
        /// </summary>
        /// <param name="id">图片Id</param>
        /// <returns></returns>
        Task<object> GetPictureLinkAsync(string id);
    }
}
