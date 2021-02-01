using HttpClients.Services.SwResolve.ApiModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.Services.SwResolve
{
    public interface IResolveClient
    {
        /// <summary>
        /// 获取授权码
        /// </summary>
        /// <param name="baseUrl">服务地址</param>
        /// <param name="user">用户名</param>
        /// <param name="password">密码</param>
        Task<AuthorizeResultModel> GetAuthorizeAsync();

        /// <summary>
        /// 获取站点列表
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <returns></returns>
        /// <param name="token">访问授权令牌</param>
        Task<StationResultModel> GetStationListAsync(int projectId, string token);

        /// <summary>
        /// 获取解算数据
        /// </summary>
        /// <param name="stationId">站点id</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="token">访问授权令牌</param>
        Task<ResolveDataResultModel> GetResolveDataAsync(int stationId, DateTime startTime, DateTime endTime, string token);
    }
}
