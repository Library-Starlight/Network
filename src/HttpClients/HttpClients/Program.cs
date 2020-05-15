using HttpClients.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await StartChargeRequestAsync();

            Console.ReadLine();
        }

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
