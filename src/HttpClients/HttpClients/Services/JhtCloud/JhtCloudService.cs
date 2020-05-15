using HttpClients.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients
{
    /// <summary>
    /// 捷慧通云平台接口协议
    /// </summary>
    public class JhtCloudService : IJhtCloud
    {
        #region 私有字段

        /// <summary>
        /// 服务地址
        /// </summary>
        private string _serviceUrl;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="baseUrl"></param>
        public JhtCloudService(string baseUrl)
        {
            _serviceUrl = "http://localhost:10095/";
        }

        #endregion

        #region 接口

        /// <summary>
        /// 获取停车位信息
        /// </summary>
        /// <param name="parkCodes">停车场编号</param>
        /// <returns></returns>
        public Task<QueryParkSpaceResponse> GetParkingSpaceAsync(string parkCodes)
        {
            var request = new QueryParkSpace
            {
                parkCodes = parkCodes,
            };

            return GetSingleItemAsync<QueryParkSpace, QueryParkSpaceResponse>(request);
        }

        /// <summary>
        /// 获取车流量信息
        /// </summary>
        /// <param name="parkCode">停车场编号</param>
        /// <param name="queryDate">查询日期</param>
        /// <returns></returns>
        public Task<QueryCurrentParkTrafficResponse> GetTrafficFlowAsync(string parkCode, DateTime queryDate)
        {
            var request = new QueryCurrentParkTraffic
            {
                parkCode = parkCode,
                queryDate = queryDate,
            };

            return GetSingleItemAsync<QueryCurrentParkTraffic, QueryCurrentParkTrafficResponse>(request);
        }

        /// <summary>
        /// 获取车辆进出记录
        /// </summary>
        /// <param name="parkCode">停车场编号</param>
        /// <param name="carNo">车牌号</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="pageIndex">查询页码</param>
        /// <param name="pageSize">查询记录数</param>
        /// <returns></returns>
        public async Task<IEnumerable<QueryParkInResponse>> GetEnterOutRecordAsync(string parkCode, string carNo, DateTime beginDate, DateTime endDate, int pageIndex, int pageSize)
        {
            var request = new QueryParkIn
            {
                parkCode = parkCode,
                carNo = carNo,
                beginDate = beginDate,
                endDate = endDate,
                pageIndex = pageIndex,
                pageSize = pageSize,
            };

            var items = await GetItemsAsync<QueryParkIn, QueryParkInResponse>(request);
            return items.Select(i => i.attributes).AsEnumerable();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="TRequest">请求</typeparam>
        /// <typeparam name="TResponse">应答</typeparam>
        /// <param name="requestBody">请求数据体</param>
        /// <returns></returns>
        private async Task<TResponse> GetSingleItemAsync<TRequest, TResponse>(TRequest requestBody)
            where TRequest : JhtCloudRequest
            where TResponse: JhtCloudResponse
        {
            var response = await GetResponseAsync<TRequest, TResponse>(requestBody);

            return response.dataItems.FirstOrDefault() ?.attributes;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="TRequest">请求数据体类型</typeparam>
        /// <typeparam name="TResponse">应答数据体类型</typeparam>
        /// <param name="requestBody">请求数据体</param>
        /// <returns></returns>
        private async Task<IEnumerable<JhtCloudResponseItem<TResponse>>> GetItemsAsync<TRequest, TResponse>(TRequest requestBody)
            where TRequest : JhtCloudRequest
            where TResponse : JhtCloudResponse
        {
            var response = await GetResponseAsync<TRequest, TResponse>(requestBody);
            return response.dataItems;
        }

        /// <summary>
        /// 获取应答
        /// </summary>
        /// <typeparam name="TRequest">请求数据体类型</typeparam>
        /// <typeparam name="TResponse">应答数据体类型</typeparam>
        /// <param name="requestBody">请求数据体</param>
        /// <returns></returns>
        private async Task<JhtCloudResponse<TResponse>> GetResponseAsync<TRequest, TResponse>(TRequest requestBody)
            where TRequest : JhtCloudRequest
            where TResponse: JhtCloudResponse
        {
            var request = new JhtCloudRequest<TRequest>()
            {
                attributes = requestBody,
            };

            var sJsonRequest = JsonConvert.SerializeObject(request);

            var url = $"{_serviceUrl}JhtCloud/Data/{request.serviceId}/";
            var sJsonResponse = await HttpRequest.PostAsync(url, sJsonRequest);

            var response = JsonConvert.DeserializeObject<JhtCloudResponse<TResponse>>(sJsonResponse);
            return response;
        }

        #endregion
    }
}
