using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TcpServerExtension
{
    public class TestTcpClient : Tcp.AsyncTcpClient
    {
        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="endPoint"></param>
        public TestTcpClient(IPEndPoint endPoint)
            : base(endPoint)
        {
            ClientDisconnected += TestTcpClient_ClientDisconnected;
            ClientReceivedData += TestTcpClient_ClientReceivedData;
        }

        private async void TestTcpClient_ClientReceivedData(object sender, Tcp.Model.ClientReceivedDataEventArgs e)
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

            await SendAsync(data);
        }

        private void TestTcpClient_ClientDisconnected(object sender, Tcp.Model.ClientDisconnectedEventArgs e)
        {
            var logMsg = $"客户端[{e.Client.RemoteEndPoint.ToString()}]连接断开。";
            Console.WriteLine(logMsg);
            Log.Logger.Instance.LogDebug(logMsg);
        }

        #endregion
    }
}
