using System;
using System.Threading.Tasks;
using Tcp;
using Tcp.Utilities;

namespace TcpServerExtension
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await StartServerAsync();
            Console.WriteLine($"按Enter退出！");
            Console.ReadLine();
        }

        static async Task StartServerAsync()
        {
            var addr = "127.0.0.1:18088";
            TcpHelper.TryParse(addr, out var ep);
            var server = TcpServerFactory.Factory.CreateTcpServer(ep);
            await server.StartAsync();
        }

        static async Task StartClientAsync()
        {
            var addr = "127.0.0.1:18088";
            TcpHelper.TryParse(addr, out var ep);
            var client = TcpClientFactory.Factory.CreateTcpClient(ep);
            await client.StartAsync();
        }
    }
}
