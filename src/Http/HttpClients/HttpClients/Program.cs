using HttpClients.Services;
using HttpClients.Services.Hikvision;
using HttpClients.Services.PartyBuild;
using HttpShared.Hikvision;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClients
{
    class Program
    {
        static async Task Main()
        {
            await GetCustomHttpResponseAsync();

            Console.ReadLine();
        }

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
