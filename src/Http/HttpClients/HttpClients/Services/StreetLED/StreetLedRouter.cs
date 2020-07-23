namespace StreetLED
{
    public static class StreetLedRouter
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        public const string BaseAddress = "http://www.led-cloud.cn";

        /// <summary>
        /// 登录地址
        /// </summary>
        public const string LoginUrl = BaseAddress + "/oauth/token";

        /// <summary>
        /// 更新令牌地址
        /// </summary>
        public const string UpdateTokenUrl = LoginUrl;

        /// <summary>
        /// 查询节目地址
        /// </summary>
        public const string GetProgramUrl = BaseAddress + "/v1/program/list";

        /// <summary>
        /// 发布节目地址
        /// </summary>
        public const string PublishProgramUrl = BaseAddress + "/v1/task/add/program";

        /// <summary>
        /// 添加指令地址
        /// </summary>
        public const string AddCommandUrl = BaseAddress + "/v1/task/add/command";

        /// <summary>
        /// 获取设备信息Url
        /// </summary>
        /// <param name="sn">设备序列号</param>
        /// <returns></returns>
        public static string GetDeviceInfoUrl(string sn)
        {
            return $"{BaseAddress}/v1/device/{sn}/info";
        }

        /// <summary>
        /// 获取设备节目Url
        /// </summary>
        /// <param name="sn">设备序列号</param>
        /// <returns></returns>
        public static string GetDeviceProgramsUrl(string sn)
        {
            return $"{BaseAddress}/v1/device/{sn}/programs";
        }

    }
}
