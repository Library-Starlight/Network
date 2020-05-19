using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpClients
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DataPipeline.Instance.ReceivedData += Instance_ReceivedData;

            var server = new Server(new Logger());
            await server.StartAsync(10077);

        }

        private static void Instance_ReceivedData(object sender, ReceivedEventArgs e)
        {
            // 日志
            var dataStr = BitConverter.ToString(e.Data).Replace("-", "");

            Console.WriteLine($"{e.Client.Socket.RemoteEndPoint}：接收数据 {dataStr}");
        }
    }
}
