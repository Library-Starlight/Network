using HttpClients.Services.SwResolve.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.Services.SwResolve
{
    /// <summary>
    /// 水务解算云接口
    /// </summary>
    public class SwResolveCloudClient
    {
        #region 常量

        /// <summary>
        /// 授权路径
        /// </summary>
        private const string AuthorizePath = "api/TokenAuth/Authenticate";

        /// <summary>
        /// 获取解算数据路径
        /// </summary>
        private const string GetResolveDataPath = "api/services/app/SolvingDataService/GetDatas";

        #endregion

        #region 公共方法

        /// <summary>
        /// 获取解算数据
        /// </summary>
        /// <param name="baseUrl">服务地址</param>
        /// <param name="stationId">站点id</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="token">访问授权令牌</param>
        public static Task<ResolveDataResultModel> GetResolveDataAsync(string baseUrl, string stationId, DateTime startTime, DateTime endTime, string token)
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "stationId", stationId },
                { "startTime", startTime.ToString("yyyy-MM-dd HH:mm:ss") },
                { "endTime", endTime.ToString("yyyy-MM-dd HH:mm:ss") },
            };

            var url = baseUrl
                .AppendRoute(GetResolveDataPath);

            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {token}" },
            };

            return JsonHttpRequest.GetFromJsonAsync<ResolveDataResultModel>(url, param: queryParameters, headers: headers);
        }

        /// <summary>
        /// 获取授权码
        /// </summary>
        /// <param name="baseUrl">服务地址</param>
        /// <param name="user">用户名</param>
        /// <param name="password">密码</param>
        public static Task<AuthorizeResultModel> GetAuthorizeAsync(string baseUrl, string user, string password)
        {
            var url = StringRouteExtensions.AppendRoute(baseUrl, AuthorizePath);

            var apiModel = new AuthorizeApiModel
            {
                UserNameOrPhoneNumber = user,
                Password = password,
                ClientType = "web",
            };

            return JsonHttpRequest.PostFromJsonAsync<AuthorizeResultModel>(url, apiModel);
        }

        #endregion
    }
}
