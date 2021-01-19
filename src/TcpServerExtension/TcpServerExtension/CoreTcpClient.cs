using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TcpServerExtension
{
    public class CoreTcpClient : Tcp.AsyncTcpClient
    {
        #region 私有字段

        /// <summary>
        /// 数据处理线程
        /// </summary>
        private Thread _thDataHandle;

        /// <summary>
        /// 数据接收标志
        /// </summary>
        private readonly AutoResetEvent _dataReceivedFlag = new AutoResetEvent(false);

        /// <summary>
        /// 数据缓存
        /// </summary>
        private readonly Queue<byte> _cache = new Queue<byte>();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="endPoint"></param>
        public CoreTcpClient(IPEndPoint endPoint)
            : base(endPoint)
        {
            ClientDisconnected += TcpClient_ClientDisconnected;
            ClientReceivedData += TcpClient_ClientReceivedData;
        }

        #endregion

        #region 重写方法

        public override Task StartAsync()
        {
            var statted = _started;
            var t = base.StartAsync();

            if (!statted)
            {
                _thDataHandle = new Thread(HandleProtocol)
                {
                    IsBackground = true,
                    Priority = ThreadPriority.Normal,
                };
                _thDataHandle.Start();
            }
            return t;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 根据协议处理数据
        /// </summary>
        private async void HandleProtocol()
        {
            Encoding encoding = Encoding.ASCII;
            List<byte> data = new List<byte>();
            byte[] lengthData = new byte[4];
            byte[] crcCheckData = new byte[4];

            while (AutoReconnect)
            {
                try
                {
                    _dataReceivedFlag.WaitOne();

                    while (_cache.Count > 0)
                    {
                        var current = _cache.Dequeue();
                        data.Add(current);
                    }

                    // 回传
                    await SendAsync(data.ToArray());
                    data.Clear();
                }
                catch (Exception ex)
                {
                    Log.Logger.Instance.LogError(ex.ToString());
                }
            }
        }

        #endregion

        #region 事件

        private void TcpClient_ClientReceivedData(object sender, Tcp.Model.ClientReceivedDataEventArgs e)
        {
            var client = e.Client;
            var data = e.Data;

            var dataStr = BitConverter.ToString(data).Replace("-", "");
            var logMsg = $"客户端[{e.Client.RemoteEndPoint.ToString()}]接收数据：0x{dataStr}";
            Console.WriteLine(logMsg);
            Log.Logger.Instance.LogDebug(logMsg);

            foreach (var b in e.Data)
                _cache.Enqueue(b);
            _dataReceivedFlag.Set();
        }

        private void TcpClient_ClientDisconnected(object sender, Tcp.Model.ClientDisconnectedEventArgs e)
        {
            var logMsg = $"客户端[{e.Client.RemoteEndPoint.ToString()}]连接断开。";
            Console.WriteLine(logMsg);
            Log.Logger.Instance.LogDebug(logMsg);
        }

        #endregion
    }
}
