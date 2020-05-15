using HttpClients.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients
{
    public class HycEquipDemo
    {
        private const string UserName = "yanshi";
        private const string Password = "user_12345";
        private const int PageIndex = 1;
        private const int PageSize = 100;

        public async Task Start()
        {
            var client = new HycClient();

            if (!client.LoginAndGetToken(UserName, Password, out var token))
                Console.WriteLine("登录失败！");

            var devices = await client.GetDevices(PageIndex, PageSize, token);

            foreach (var device in devices.Values)
                Console.WriteLine($"设备imei：{device.IMEI}, 0号插口{device.PlugStatus0}, 1号插口{device.PlugStatus1}");
        }
    }
}
