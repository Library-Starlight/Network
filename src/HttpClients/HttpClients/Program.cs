using HttpClients.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients
{
    class Program
    {
        static void Main(string[] args)
        {
            StartRequest();
            Console.ReadLine();
        }

        private static async void StartRequest()
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
    }
}
