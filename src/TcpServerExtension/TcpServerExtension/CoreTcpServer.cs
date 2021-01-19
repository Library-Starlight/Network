using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TcpServerExtension
{
    /// <summary>
    /// Tcp服务器，将二进制数据为易用的实体类
    /// </summary>
    public class CoreTcpServer : Tcp.AsyncTcpServer
    {
        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="ep"></param>
        public CoreTcpServer(IPEndPoint ep)
            : base(ep)
        {
            ClientConnected += TcpServer_ClientConnected;
            ClientDisconnected += TcpServer_ClientDisconnected;
            ClientReceivedData += TestTcpServer_ClientReceivedData;
        }

        #endregion

        #region 事件

        /// <summary>
        /// 数据接收事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 连接断开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TcpServer_ClientDisconnected(object sender, Tcp.Model.ClientDisconnectedEventArgs e)
        {
            var logMsg = $"客户端[{e.Client.RemoteEndPoint.ToString()}]连接断开。";
            Console.WriteLine(logMsg);
            Log.Logger.Instance.LogDebug(logMsg);
        }

        /// <summary>
        /// 连接成功事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TcpServer_ClientConnected(object sender, Tcp.Model.ClientConnectedEventArgs e)
        {
            var logMsg = $"客户端[{e.Client.RemoteEndPoint.ToString()}]连接成功。";
            Console.WriteLine(logMsg);
            Log.Logger.Instance.LogDebug(logMsg);
        }

        #endregion
    }
}
