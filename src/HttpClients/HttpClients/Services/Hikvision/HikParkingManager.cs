using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.Services.Hikvision
{
    /// <summary>
    /// 停车场管理系统
    /// </summary>
    public class HikParkingManager //: IHikParkingManager
    {
        #region 常量

        /// <summary>
        /// 获取剩余停车位路径
        /// </summary>
        private const string GetSpacePath = "/api/pms/v1/park/remainSpaceNum";

        /// <summary>
        /// 获取停车记录路径
        /// </summary>
        private const string GetParkStatPath = "/api/pms/v1/crossRecords/page";

        /// <summary>
        /// 获取图片链接路径
        /// </summary>
        private const string GetPicturePath = "/api/pms/v1/image";

        #endregion

        #region 私有字段

        /// <summary>
        /// 带认证的请求构造类
        /// </summary>
        private readonly HikAuthRequestBuilder _requestBuilder;

        /// <summary>
        /// Http请求基地址
        /// </summary>
        private readonly Uri _baseAddress;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public HikParkingManager(string baseAddress)
        {
            _baseAddress = new Uri(baseAddress);
            _requestBuilder = new HikAuthRequestBuilder();
        }

        #endregion

        #region 接口实现

        /// <summary>
        /// 获取剩余车位数
        /// </summary>
        /// <returns></returns>
        public Task<object> GetSpacesAsync()
        {
            return PostAsync<object>(GetSpacePath, "{}");
        }

        #endregion

        #region 私有成员

        /// <summary>
        /// 发送Post请求，并获取应答内容
        /// </summary>
        /// <param name="path">Uri相对路径</param>
        /// <param name="content">请求内容</param>
        /// <returns>应答内容</returns>
        private async Task<T> PostAsync<T>(string path, string content)
        {
            // Http客户端
            var client = new HttpClient { BaseAddress = _baseAddress };

            // 构造含认证信息Http请求
            var request = _requestBuilder.BuildRequest(path, content);

            // 发送请求
            var response = await client.SendAsync(request);

            // 应答内容
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        #endregion
    }
}
