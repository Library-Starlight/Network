using HttpClients.Services;
using HttpClients.Services.Hikvision;
using HttpClients.Services.PartyBuild;
using Newtonsoft.Json;
using StreetLED;
using StreetLED.Model.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients
{
    class Program
    {
        static async Task Main()
        {
            try
            {
                await JsonRequestAsync();

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #region 通用Json请求及应答

        public static async Task JsonRequestAsync()
        {
            const string username = "zhongshanStreet";
            const string password = "King888888";
            const string deviceSN = "C35-C19-A0491";

            var credentials = await StreetLedApi.GetCredentialsAsync(username, password);
            Console.WriteLine(JsonConvert.SerializeObject(credentials, Formatting.Indented));

            var token = credentials.access_token;

            //// 获取设备信息
            //var device = await StreetLedApi.GetDeviceInfoAsync(token, deviceSN);
            //Console.WriteLine(JsonConvert.SerializeObject(device, Formatting.Indented));

            // 获取设备节目
            var devicePrograms = await StreetLedApi.GetDeviceProgramsAsync(token, deviceSN);
            Console.WriteLine(JsonConvert.SerializeObject(devicePrograms, Formatting.Indented));

            //// 获取节目信息
            //var programs = await StreetLedApi.GetProgramsAsync(token);
            //Console.WriteLine(JsonConvert.SerializeObject(programs, Formatting.Indented));

            //// 发布节目
            //var sns = new List<string> { "1", "2" };
            //var programIds = new List<int> { 1, 2 };
            //var pubResult = await StreetLedApi.PublishProgramAsync(token, sns, programIds);
            //Console.WriteLine(pubResult);

            //// 发送指令
            //var devices = new List<string> { "1", "2" };
            //var command = CommandType.open;
            //var sendResult = await StreetLedApi.SendCommmandAsync(token, command, devices);
            //Console.WriteLine(sendResult);

            //// 设置亮度
            //var setBrightnessResult = await StreetLedApi.UpdateBrightnessAsync(token, deviceSN, 50);
            //Console.WriteLine(setBrightnessResult);

            //// 设置音量
            //var setVolumeResult = await StreetLedApi.UpdateVolumeAsync(token, deviceSN, 50);
            //Console.WriteLine(setVolumeResult);
        }

        private static void Serialize<TRequest>(TRequest request)
        {
            Console.WriteLine(JsonConvert.SerializeObject(request));
        }

        #endregion

        #region x-www-form-urlencoded请求

        public static async Task FormUrlEncodedRequestAsync()
        {
            var url = "http://www.led-cloud.cn/oauth/token";
            var parameters = new Dictionary<string, string>
            {
                {"client_secret" , "dms-browser"},
                {"client_id" , "dms-browser"},
                {"grant_type" , "password"},
                {"username" , "chenjilan123"},
                {"password" , "Qz8954167"},
                {"scope" , "all"},
            };

            var response = await JsonHttpRequest.PostFormDataFromJsonAsync<UserCredentialsDataModel>(url, parameters);

            var str = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(str);
        }

        #endregion

        #region 获取自定义Http应答

        private static async Task GetCustomHttpResponseAsync()
        {
            const string uri = "https://www.taxiaides.com/xyyc_sdk_api/rest/query/result?operationTaskId=62PkT8dI3SdNd0476XB650i8CT450QLc";
            var client = new HttpClient();
            var response = await client.GetAsJsonAsync<HycSearchTaskResultResponse>(uri);
            
            Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
            
            var data = response.ServerResponse.data;
            var devices = JsonConvert.DeserializeObject<HycTaskResultData>(data);

            var formatted = JsonConvert.SerializeObject(devices, Formatting.Indented);
            Console.WriteLine(formatted);

            // 写入到文件
            using (var fs = new FileStream("devices.json", FileMode.OpenOrCreate, FileAccess.Write))
            using (var sw = new StreamWriter(fs))
            {
                sw.Write(formatted);
            }
        }

        #endregion

        #region 包含ContentMD5和X-Ca-Signature的Http请求

        private static void HikHttpRequest()
        {
            new HikOpenAPI().Send();
        }

        #endregion

        #region 获取包含令牌的页面访问Uri

        private static async Task GetUriWithToken()
        {
            var provider = new PartyBuildPageProvider();

            //var uri = await provider.GetAccessPageUri();
            //Console.WriteLine(uri);

            Console.WriteLine($"党员统计：");
            await provider.GetPartySummaryAsync();
            Console.WriteLine();

            Console.WriteLine($"楼层党员统计");
            await provider.GetPartyOfBuilding();
            Console.WriteLine();

            Console.WriteLine($"楼层党员统计：1楼");
            await provider.GetPartyOfBuilding(1);
        }

        #endregion

        #region 自定义Json库

        private static async Task CustomJsonLibraryAsync()
        {
            var client = new HttpClient();
            var resultMessage = await client.PostFromJsonAsync("https://localhost:44367/Hikvision", new Authenticate { AppKey = "fadsjjlfhsajkrevnf1180", AppSecret = "gds6_das363dsijrlsalfdv1&=" });

            Console.WriteLine($"StatusCode: {resultMessage.StatusCode}({(int)resultMessage.StatusCode})");
            var result = await resultMessage.Content.ReadAsStringAsync();
            
            Console.WriteLine(result);
        }

        #endregion

        #region 官方Http请求Json库

        private static async Task SystemNetHttpJson()
        {
            var client = new HttpClient();
            var result = await client.GetFromJsonAsync<List<WeatherForecast>>("https://localhost:44367/api/WeatherForecast");

            foreach (var item in result)
                Console.WriteLine(item.Summary);
        }

        #endregion

        #region 开始Http请求

        private static async Task StartChargeRequestAsync()
        {
            try
            {
                await new HycEquipDemo().Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static async void StarParkingSystemRequestAsync()
        {
            IJhtCloud service = new JhtCloudService("http://127.0.0.1:10095/");

            // 获取空闲停车位
            var parkSpace = await service.GetParkingSpaceAsync("v3_1");
            PrintObjectDecent(parkSpace);

            // 获取车流量统计
            var traffic = await service.GetTrafficFlowAsync("v3_1", DateTime.Now.Date);
            PrintObjectDecent(traffic);

            // 获取进场车辆信息
            var parkIn = await service.GetEnterOutRecordAsync("v3_1", string.Empty, DateTime.Now.Date, DateTime.Now.Date, 0, 100);
            PrintObjectDecent(parkIn);
        }

        private static void PrintObjectDecent<T>(T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            Console.WriteLine(json);
        }

        #endregion

        #region 组合HttpGet数据体

        private static void BuildHttpGetBody()
        {
            //var strs = new string[]
            //{
            //    "13", "hehe", "gage", "gage",
            //};
            //var aggr = strs.Aggregate((prev, cur) => $"{prev}, {cur}");
            //Console.WriteLine(aggr);
            //return;

            var param = new Dictionary<string, string>
            {
                { "param1", "133" },
                { "param2", "133" },
                { "param3", "133" },
                { "name", "左岸的咖啡！" },
            };
            var result = AppendHttpGetParam("http://localhost", param);
            Console.WriteLine(result);
        }

        private static string AppendHttpGetParam(string url, IDictionary<string, string> param)
        {
            // 若无参数，则返回url
            if (param == null || param.Count <= 0)
                return url;

            var sb = new StringBuilder();
            sb.Append(url);
            var first = true;
            foreach (var kv in param)
            {
                // 对数据进行Url编码
                var encodedValue = WebUtility.UrlEncode(kv.Value);
                if (!first)
                {
                    sb.Append($"&{kv.Key}={encodedValue}");
                }
                else
                {
                    sb.Append($"?{kv.Key}={encodedValue}");
                    first = false;
                }
            }
            return sb.ToString();

            //return url + param.Aggregate("?", (prev, kv) =>
            //{
            //    if (prev == "?")
            //        return prev + $"{kv.Key}={WebUtility.UrlEncode(kv.Value)}";
            //    return $"{prev}&{kv.Key}={WebUtility.UrlEncode(kv.Value)}";
            //});
        }

        #endregion
    }
}
