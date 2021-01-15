using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tcp;

namespace Tcp
{
    /// <summary>
    /// Tcp服务器，将二进制数据为易用的实体类
    /// </summary>
    public class TestTcpServer : AsyncTcpServer
    {
        public TestTcpServer(IPEndPoint ep)
            : base(ep)
        {
            ClientConnected += TestTcpServer_ClientConnected;
            ClientDisconnected += TestTcpServer_ClientDisconnected;
            ClientReceivedData += TestTcpServer_ClientReceivedData;
        }

        private async void TestTcpServer_ClientReceivedData(object sender, Tcp.Model.ClientReceivedDataEventArgs e)
        {
            var client = e.Client;
            var data = e.Data;

            var dataStr = BitConverter.ToString(data).Replace("-", "");
            var logMsg = $"客户端[{e.Client.RemoteEndPoint.ToString()}]接收数据：0x{dataStr}";
            Console.WriteLine(logMsg);
            Log.Logger.Instance.LogDebug(logMsg);

            if (data[0] == 0x30)
            {
                client.Close();
                return;
            }

            await client.SendAsync(data);
        }

        private void TestTcpServer_ClientDisconnected(object sender, Tcp.Model.ClientDisconnectedEventArgs e)
        {
            var logMsg = $"客户端[{e.Client.RemoteEndPoint.ToString()}]连接断开。";
            Console.WriteLine(logMsg);
            Log.Logger.Instance.LogDebug(logMsg);
        }

        private void TestTcpServer_ClientConnected(object sender, Tcp.Model.ClientConnectedEventArgs e)
        {
            var logMsg = $"客户端[{e.Client.RemoteEndPoint.ToString()}]连接成功。";
            Console.WriteLine(logMsg);
            Log.Logger.Instance.LogDebug(logMsg);

            //var data = Encoding.ASCII.GetBytes("Hello World!");
            //e.Client.SendAsync(data);
        }
    }
}
