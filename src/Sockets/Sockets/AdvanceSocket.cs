using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Sockets
{
    public class AdvanceSocket
    {
        public static void What()
        {
            Socket socket;

            TcpClient tcp;

            UdpClient udp;

            IPHostEntry hostInfo = Dns.GetHostEntry("192.168.0.138");

            Console.WriteLine($"主机：{hostInfo.HostName}");
            for (int i = 0; i < hostInfo.AddressList.Length; i++)
            {
                Console.WriteLine($"地址：{hostInfo.AddressList[i]}");
            }

            // IPAddress不可序列化
            //JsonConvert.SerializeObject(IPAddress.Loopback);
            //Console.WriteLine(JsonSerializer.Serialize(IPAddress.Loopback, new JsonSerializerOptions { WriteIndented = true }));
            //Console.WriteLine(JsonSerializer.Serialize(host, new JsonSerializerOptions { WriteIndented = true }));
        }

        public static void Udp()
        {

        }

        static void Main(string[] args)
        {
            What();
        }
    }
}
