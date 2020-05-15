using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.Services
{
    public class HycHttpClient
    {
        #region 系统名称

        /// <summary>
        /// 系统名称
        /// </summary>
        private const string SystemName = "和易充智慧充电桩";

        #endregion

        #region 服务地址

        /// <summary>
        /// Http服务器基地址
        /// </summary>
        // TODO: 实际使用时，修改为www.taxiaides.com
        private const string BaseAddress = "localhost:10095";

        /// <summary>
        /// 登录Url
        /// </summary>
        private const string LoginUrl = "http://" +  BaseAddress + "/xyyc_sdk_api/rest/user/agentLogin";

        /// <summary>
        /// 获取设备信息Url
        /// </summary>
        private const string GetDevicesUrl = "http://" + BaseAddress + "/xyyc_sdk_api/rest/device/getDevices";

        /// <summary>
        /// 查询任务结果Url
        /// </summary>
        private const string QueryResultUrl = "http://" + BaseAddress + "/xyyc_sdk_api/rest/query/result";

        #endregion

        #region 公共方法

        /// <summary>
        /// 登录并获取用户令牌
        /// </summary>
        /// <param name="url">服务器地址</param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool LoginAndGetToken(string username, string password, out string token)
        {
            var loginRequest = new HycLogin
            {
                userName = username,
                password = password,
            };

            var requestBody = JsonConvert.SerializeObject(loginRequest, Formatting.None);
            var responseStr = HttpRequest.PostAsync(LoginUrl, requestBody).Result;

            var response = JsonConvert.DeserializeObject<HycLoginResponse>(responseStr);

            if (response == null || response.code != HycResultCode.Success)
            {
                token = default;
                return false;
            }
            else
            {
                token = response.result.token;
                return true;
            }
        }

        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public async Task<IDictionary<string, HycDevice>> GetDevices(int pageIndex, int pageSize)
        {
            var getDevRequest = new HycGetDevices
            {
                currentPage = pageIndex,
                // 查询设备类型为NB设备
                devType = HycDeviceType.NB,
                itemsPerPage = pageSize,
            };

            // 查询NB设备信息
            var getDevicesResult = await PostResultAsync<HycGetDevices, HycGetDevicesResponse>(GetDevicesUrl, getDevRequest);
            if (getDevicesResult == null || string.IsNullOrEmpty(getDevicesResult.operationTaskId))
                return new Dictionary<string, HycDevice>();

            // 等待查询结果
            IDictionary<string, string> queryResultParam = new Dictionary<string, string>
            {
                // 设置任务Id
                { "operationTaskId", getDevicesResult.operationTaskId },
            };

            var beginTime = DateTime.Now;

            // 30秒内等待结果，超过30秒后超时退出
            while ((DateTime.Now - beginTime).TotalSeconds <= 30D)
            {

                var response = await GetResultAsync<HycSearchTaskResultResponse>(QueryResultUrl, queryResultParam);

                // 查询成功
                if (response.code == HycResultCode.Success)
                {
                    // 正在执行
                    if (response.operationStatus == HycOperationStatus.running)
                    {
                        // 服务器仍在查询中，等待3秒后再次确认。
                        await Task.Delay(TimeSpan.FromSeconds(3D));
                        continue;
                    }
                    // 查询成功
                    else if (response.operationStatus == HycOperationStatus.success)
                    {
                        return GetDevicesData(response);
                    }
                    else
                    {
                        // 查询失败
                        Console.WriteLine($"{SystemName}：查询设备状态失败，失败原因：{response.code.ToString()}，页码：{pageIndex}，记录数：{pageSize}");
                    }
                }
                else
                {
                    // 请求失败
                    Console.WriteLine($"{SystemName}：请求设备状态失败，页码：{pageIndex}，记录数：{pageSize}");
                }

                // 查询成功，查询失败或请求失败时，退出循环
                break;
            }

            // 如果到达这里，表示失败
            return new Dictionary<string, HycDevice>();
        }

        /// <summary>
        /// 获取设备数据
        /// </summary>
        /// <param name="response">从服务器上查询成功的应答</param>
        /// <returns></returns>
        private IDictionary<string, HycDevice> GetDevicesData(HycSearchTaskResultResponse response)
        {
            return response.data.rows.Select(row => new HycDevice
            {
                IMEI = row.devImei,
                DevType = row.devType,
                StationName = row.stationName,
                McName = row.mchName,
                LogStatus = row.logStatus,
                AddDate = row.addDate,
                OneNetDevId = row.onenetDevId,
                Online = row.online == HycOnlineStatus.在线,
                PlugStatus0 = row.plugs != null && row.plugs.Length >= 1 ? row.plugs[0].plugStatus : HycPlugStatus.空闲,
                PlugStatus1 = row.plugs != null && row.plugs.Length >= 2 ? row.plugs[1].plugStatus : HycPlugStatus.空闲,
            }).ToDictionary(dev => dev.IMEI);
        }

        #endregion

        #region 私有成员

        /// <summary>
        /// 获取Post请求结果
        /// </summary>
        /// <typeparam name="TRequest">请求类型</typeparam>
        /// <typeparam name="TResponse">应答类型</typeparam>
        /// <param name="url">请求完整地址</param>
        /// <param name="request">请求数据</param>
        /// <returns></returns>
        private async Task<TResponse> PostResultAsync<TRequest, TResponse>(string url, TRequest request)
        {
            var requestBody = JsonConvert.SerializeObject(request);
            var responseStr = await HttpRequest.PostAsync(url, requestBody);

            Console.WriteLine($"{SystemName}Post请求，Url：{url}，应答：{responseStr}");

            return JsonConvert.DeserializeObject<TResponse>(responseStr);
        }

        /// <summary>
        /// 获取Get请求结果
        /// </summary>
        /// <typeparam name="T">应答类型</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="param">请求参数</param>
        /// <returns></returns>
        private async Task<T> GetResultAsync<T>(string url, IDictionary<string, string> param)
        {
            var responseStr = await HttpRequest.GetAsync(QueryResultUrl, param);

            Console.WriteLine($"{SystemName}Get请求，Url：{url}，应答：{responseStr}");

            return JsonConvert.DeserializeObject<T>(responseStr);
        }

        #endregion
    }
}
