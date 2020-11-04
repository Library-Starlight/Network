using HttpClients.Services;
using HttpClients.Services.Hikvision;
using HttpClients.Services.JhpjGeo;
using HttpClients.Services.PartyBuild;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StreetLED;
using StreetLED.Model.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
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
                Console.WriteLine($"请输入服务地址和设备号（用逗号分隔）:");
                var parameters = Console.ReadLine().Split(',');

                while (true)
                {
                    await RequestJhpjGeomagnetismAsync(parameters);
                    await Task.Delay(500);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }

        #region 金华浦江地磁

        private static async Task RequestJhpjGeomagnetismAsync(string[] parameters)
        {
            var hostUrl = parameters[0];
            var id = parameters[1];

            await JhpjGeoClient.Request(hostUrl, id);
        }

        #endregion

        #region 张家港天气局

        private static async Task ZjgWeatherApi()
        {
            var appid = "E4628CE3ED4B4057A1D06404B083AD9B";
            var secret = "645D3B6DEAE64ED0BD3512D9CEFAE0EF";

            var token = await ZjgApi.ZjgWeatherAccessTokenApi.GetTokenAsync("http://www.zjg121.com/zjgqxj2/", appid, secret);
            Console.WriteLine(token);

            var url = "http://www.zjg121.com/zjgqxj2/WeatherService/Station.ashx";
            var parameters = new Dictionary<string, string>
            {
                { "appid", "E4628CE3ED4B4057A1D06404B083AD9B" },
                { "token", token },
            };

            var message = await HttpRequest.GetAsync(url, parameters);

            var jArr = JArray.Parse(message);
            Console.WriteLine(jArr.ToString(Newtonsoft.Json.Formatting.Indented));
        }

        #endregion

        #region 无序列化请求及应答

        static async Task RequestPureJson()
        {
            // 获取设备状态
            {
                // address
                var uri = "http://testahome.iot.wanyol.com/inner-direct/v1/iot/device/status";
                // body
                var body = "{\"devices\":[]}";
                // header
                var headers = GetHeader(new Dictionary<string, string>(), body);

                var response = await HttpRequest.PostAsync(uri, headers: headers, body: body);

                var jObj = JObject.Parse(response);
                Console.WriteLine(jObj.ToString(Formatting.Indented));

                //var client = new HttpClient();
                //var content = new StringContent(body);
                //foreach (var header in headers)
                //    content.Headers.Add(header.Key, header.Value);

                //// response
                //var response = await client.PostAsync(uri, content);

                //var responseStr = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(responseStr);
            }
            return;

            // 获取设备列表
            {
                //// address
                var uri = "http://testahome.iot.wanyol.com/inner-direct/v1/iot/device/list";

                // parameter
                var parameters = new Dictionary<string, string>
                {
                    { "pageNum", "0" },
                    { "pageSize", "10" },
                    { "domainId", "1" },
                };

                // headers
                var headers = GetHeader(parameters, string.Empty);

                var response = await HttpRequest.GetAsync(uri, param: parameters, headers: headers);
                Console.WriteLine(response);

                //var client = new HttpClient();
                //var content = new StringContent(string.Empty);
                //content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                //foreach (var header in headers)
                //    content.Headers.Add(header.Key, header.Value);

                //var response = await client.PostAsync(uri, content);
                //Console.WriteLine(await response.Content.ReadAsStringAsync());
            }

            // 获取BindKey
            {
                var uri = "http://testahome.iot.wanyol.com/inner-direct/v1/iot/device/bindkey";
                var client = new HttpClient();

                var parameters = new Dictionary<string, string>
                {
                        { "pid", "rtaK" },
                        { "domainId", "1" },
                        { "type", "1" },
                };

                var headers = GetHeader(parameters, string.Empty);

                var response = await HttpRequest.GetAsync(uri, param: parameters, headers: headers);
                Console.WriteLine(response);

                //var content = new StringContent(string.Empty);
                //foreach (var h in headers)
                //    content.Headers.Add(h.Key, h.Value);

                //var response = await client.PostAsync(uri, content);
                //Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// 生成请求头
        /// </summary>
        /// <param name="body">请求消息体</param>
        /// <returns></returns>
        private static Dictionary<string, string> GetHeader(Dictionary<string, string> parameters, string body)
        {
            var timestamp = DateTime.UtcNow.ToSecondInt64().ToString();
            var random = new Random(Guid.NewGuid().ToString().GetHashCode()).Next(0, 10000).ToString();

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("appid", "128335875");
            headers.Add("nonce", random);
            headers.Add("timestamp", timestamp);
            headers.Add("signature", Signature(parameters, headers, body));
            return headers;
        }

        /// <summary>
        /// 参数签名
        /// </summary>
        /// <param name="requestParms"></param>
        /// <param name="bodyStr"></param>
        /// <returns></returns>
        private static string Signature(Dictionary<string, string> parameters, Dictionary<string, string> headers, string bodyStr)
        {
            var requestParms = new Dictionary<string, string>();
            foreach (var item in parameters)
                requestParms.Add(item.Key, item.Value);
            foreach (var item in headers)
                requestParms.Add(item.Key, item.Value);
            var orderArray = requestParms.OrderBy(p => p.Key);
            var strArr = orderArray.Select(p => $"{p.Key}={p.Value}");
            var fullstr = string.Join("&", strArr) + bodyStr;
            Console.WriteLine(fullstr);
            var sha = HmacSHA256Encrypt(fullstr);
            return Convert.ToBase64String(sha).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }

        /// <summary>
        /// HmacSHA256加密
        /// </summary>
        /// <param name="message">明文</param>
        /// <param name="secret">密钥</param>
        /// <returns>密文</returns>
        private static byte[] HmacSHA256Encrypt(string message)
        {
            var encoding = new UTF8Encoding();
            var keyByte = encoding.GetBytes("1234567890abcabc");
            var messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                var hashmessage = hmacsha256.ComputeHash(messageBytes);
                return hashmessage;
            }
        }

        #endregion

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
