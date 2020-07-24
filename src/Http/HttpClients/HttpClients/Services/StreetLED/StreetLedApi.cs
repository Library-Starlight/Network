using StreetLED.Model;
using StreetLED.Model.Enums;
using StreetLED.Model.Request;
using StreetLED.Model.Response;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace StreetLED
{
    public static class StreetLedApi
    {
        #region 公共方法

        /// <summary>
        /// 获取身份认证信息
        /// </summary>
        /// <param name="username">身份证</param>
        /// <param name="password">密码</param>
        /// <returns>身份信息</returns>
        public static async Task<UserCredentialsDataModel> GetCredentialsAsync(string username, string password)
        {
            var parameters = new Dictionary<string, string>
            {
                {"client_secret" , "dms-browser"},
                {"client_id" , "dms-browser"},
                {"grant_type" , "password"},
                {"username" , username },
                {"password" , password },
                {"scope" , "all"},
            };

            UserCredentialsDataModel credentials;
            try
            {
                credentials = await JsonHttpRequest.PostFormDataFromJsonAsync<UserCredentialsDataModel>(StreetLedRouter.LoginUrl, parameters);
            }
            catch 
            {
                credentials = null;
            }

            return credentials;
        }

        /// <summary>
        /// 获取设备
        /// </summary>
        /// <param name="sn">设备序列号</param>
        /// <param name="token">Http请求令牌</param>
        /// <returns>设备信息</returns>
        public static Task<DeviceDataModel> GetDeviceInfoAsync(string token, string sn)
        {
            var url = StreetLedRouter.GetDeviceInfoUrl(sn);
            return GetAsync<DeviceDataModel>(url, token);
        }

        /// <summary>
        /// 获取设备节目
        /// </summary>
        /// <param name="sn">设备序列号</param>
        /// <param name="token">Http请求令牌</param>
        /// <returns>设备信息</returns>
        public static Task<StreetLedPagedModel<DeviceProgramsDataModel>> GetDeviceProgramsAsync(string token, string sn)
        {
            var url = StreetLedRouter.GetDeviceProgramsUrl(sn);
            return GetAsync<StreetLedPagedModel<DeviceProgramsDataModel>>(url, token);
        }

        /// <summary>
        /// 获取节目列表
        /// </summary>
        /// <param name="token">Http请求令牌</param>
        /// <returns></returns>
        public static Task<StreetLedPagedModel<ProgramItem>> GetProgramsAsync(string token)
        {
            return GetAsync<StreetLedPagedModel<ProgramItem>>(StreetLedRouter.GetProgramUrl, token);
        }

        /// <summary>
        /// 发布节目
        /// </summary>
        /// <param name="sns">设备列表</param>
        /// <param name="programIds">节目列表</param>
        /// <returns>发布结果</returns>
        public static async Task<bool> PublishProgramAsync(string token, List<string> sns, List<int> programIds, bool interlude = false, bool reset = false)
        {
            var request = new PublishProgramApiModel
            {
                interlude = interlude,
                reset = reset,
                programs = programIds,
                devices = sns,
            };

            var response = await PostAsync<TaskDataModel>(StreetLedRouter.PublishProgramUrl, request, token);
            return response != null;
        }

        /// <summary>
        /// 发送指令
        /// </summary>
        /// <param name="command">指令类型</param>
        /// <param name="sns">设备列表</param>
        /// <returns>发送结果</returns>
        public static async Task<bool> SendCommmandAsync(string token, CommandType command, List<string> sns)
        {
            var request = new AddCommandApiModel
            {
                command = command.ToString(),
                devices = sns,
            };
            var response = await PostAsync<TaskDataModel>(StreetLedRouter.AddCommandUrl, request, token);
            return response != null;
        }

        /// <summary>
        /// 更新亮度
        /// </summary>
        /// <param name="token">Http请求令牌</param>
        /// <param name="brightness">亮度大小，范围：0-100</param>
        /// <returns></returns>
        public static async Task<bool> UpdateBrightnessAsync(string token, string sn, int brightness)
        {
            if (brightness < 0 || brightness > 100)
                return false;

            // 创建更新亮度的请求
            var request = new UpdateSettingsApiModel
            {
                settings = new
                {
                    brightness = new
                    {
                        mode = "fixed",
                        @fixed = brightness,
                    },
                },
                devices = new List<string> { sn },
            };

            var response = await PostAsync<TaskDataModel>(StreetLedRouter.AddSettingsUrl, request, token);

            return response != null;
        }

        /// <summary>
        /// 更新音量
        /// </summary>
        /// <param name="token">Http请求令牌</param>
        /// <param name="volume">音量大小，范围：0-100</param>
        /// <returns></returns>
        public static async Task<bool> UpdateVolumeAsync(string token, string sn, int volume)
        {
            if (volume < 0 || volume > 100)
                return false;

            // 创建更新音量的请求
            var request = new UpdateSettingsApiModel
            {
                settings = new { volume },
                devices = new List<string> { sn },
            };

            var response = await PostAsync<TaskDataModel>(StreetLedRouter.AddSettingsUrl, request, token);

            return response != null;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取Bearer Authorization请求头部
        /// </summary>
        /// <param name="token">Http请求令牌</param>
        /// <returns></returns>
        private static IDictionary<string, string> GetHeaders(string token)
        {
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {token}" },
            };
            return headers;
        }

        /// <summary>
        /// Http Get请求
        /// </summary>
        /// <typeparam name="TResponse">应答类型</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="token">用户令牌</param>
        /// <returns>应答内容</returns>
        private static async Task<TResponse> GetAsync<TResponse>(string url, string token)
        {
            var headers = GetHeaders(token);
            try
            {
                var response = await JsonHttpRequest.GetFromJsonAsync<StreetLedApiResponse<TResponse>>(url, headers: headers);
                if (response == null || response.code != 200)
                    return default;

                return response.data;
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Http Post请求
        /// </summary>
        /// <typeparam name="TRequest">请求类型</typeparam>
        /// <typeparam name="TResponse">应答类型</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="token">用户令牌</param>
        /// <returns>应答内容</returns>
        private static async Task<TResponse> PostAsync<TResponse>(string url, object request, string token)
        {
            var headers = GetHeaders(token);
            try
            {
                var response = await JsonHttpRequest.PostFromJsonAsync<StreetLedApiResponse<TResponse>>(url, request, headers: headers);
                if (response == null || response.code != 200)
                    return default;

                return response.data;
            }
            catch 
            {
                return default;
            }
        }

        #endregion
    }
}
