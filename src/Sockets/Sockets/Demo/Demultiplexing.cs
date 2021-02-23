using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Sockets.Demo
{
    /// <summary>
    /// 多地址监听
    /// </summary>
    public class Demultiplexing
    {
        public async Task StartAsync()
        {
            foreach (var item in System.Net.NetworkResolver.ResolveNetworkInterfaces())
            {
                System.Console.WriteLine(item);
            }

            var server1 = new TcpListener(8085);
            server1.Start();
            var server2 = new TcpListener(IPAddress.Loopback, 8085);
            server2.Start();
            var server3 = new TcpListener(IPAddress.Parse("192.168.0.103"), 8085);
            server3.Start();

            var t1 = server1.AcceptTcpClientAsync();
            var t2 = server2.AcceptTcpClientAsync();
            var t3 = server3.AcceptTcpClientAsync();
            System.Console.WriteLine($"TaskId1: {t1.Id.ToString()}");
            System.Console.WriteLine($"TaskId2: {t2.Id.ToString()}");
            System.Console.WriteLine($"TaskId3: {t3.Id.ToString()}");

            // var client = new TcpClient("127.0.0.1", 8085);
            var client = new TcpClient("192.168.0.103", 8085);
            System.Console.WriteLine($"Success");

            var c = await Task.WhenAny(t1, t2, t3);
            // var c = await Task.WhenAny(t1, t2);
            var actualClient = c.Result;
            System.Console.WriteLine($"TaskId: {c.Id.ToString()}");
            System.Console.WriteLine($"{actualClient.Client.RemoteEndPoint.ToString()}");
        }
    }
}
