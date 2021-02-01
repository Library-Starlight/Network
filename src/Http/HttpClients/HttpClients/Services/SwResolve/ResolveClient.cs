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
    public class ResolveClient : IResolveClient
    {
        #region 常量

        /// <summary>
        /// 授权路径
        /// </summary>
        private const string AuthorizePath = "api/TokenAuth/Authenticate";

        /// <summary>
        /// 获取站点列表路径
        /// </summary>
        private const string GetStationPath = "api/services/app/Mgr_StationService/GetAllList";

        /// <summary>
        /// 获取解算数据路径
        /// </summary>
        private const string GetResolveDataPath = "api/services/app/SolvingDataService/GetDatas";

        #endregion

        #region 私有字段

        /// <summary>
        /// 服务地址
        /// </summary>
        private readonly string _baseUrl;

        /// <summary>
        /// 用户名
        /// </summary>
        private readonly string _username;

        /// <summary>
        /// 密码
        /// </summary>
        private readonly string _password;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="baseUrl">服务地址</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public ResolveClient(string baseUrl, string username, string password)
        {
            _baseUrl = baseUrl;
            _username = username;
            _password = password;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 获取授权码
        /// </summary>
        /// <param name="baseUrl">服务地址</param>
        /// <param name="user">用户名</param>
        /// <param name="password">密码</param>
        public Task<AuthorizeResultModel> GetAuthorizeAsync()
        {
            var url = _baseUrl.AppendRoute(AuthorizePath);
            var apiModel = new AuthorizeApiModel
            {
                UserNameOrPhoneNumber = _username,
                Password = _password,
                ClientType = "web",
            };

            return JsonHttpRequest.PostFromJsonAsync<AuthorizeResultModel>(url, apiModel);
        }

        /// <summary>
        /// 获取站点列表
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <param name="token">访问授权令牌</param>
        /// <returns></returns>
        public Task<StationResultModel> GetStationListAsync(int projectId, string token)
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "projectId", projectId.ToString() },
            };
            var url = _baseUrl.AppendRoute(GetStationPath);
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {token}" },
            };


            return JsonHttpRequest.GetFromJsonAsync<StationResultModel>(url, param: queryParameters, headers:headers );
        }

        /// <summary>
        /// 获取解算数据
        /// </summary>
        /// <param name="stationId">站点id</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="token">访问授权令牌</param>
        public Task<ResolveDataResultModel> GetResolveDataAsync(int stationId, DateTime startTime, DateTime endTime, string token)
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "stationId", stationId.ToString() },
                { "startTime", startTime.ToString("yyyy-MM-dd HH:mm:ss") },
                { "endTime", endTime.ToString("yyyy-MM-dd HH:mm:ss") },
            };
            var url = _baseUrl
                .AppendRoute(GetResolveDataPath);
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {token}" },
            };

            return JsonHttpRequest.GetFromJsonAsync<ResolveDataResultModel>(url, param: queryParameters, headers: headers);
        }

        #endregion
    }
}
