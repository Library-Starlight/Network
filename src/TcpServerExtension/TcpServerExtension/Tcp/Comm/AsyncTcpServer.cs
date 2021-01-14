using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace System.Net
{ 
    /// <summary>
    /// 异步的Tcp通讯服务器，处理传输层数据的接收与转发
    /// </summary>
    public abstract class AsyncTcpServer
    {
        #region 私有字段

        /// <summary>
        /// 线程同步锁
        /// </summary>
        private readonly object _objLock = new object();

        /// <summary>
        /// Tcp服务端
        /// </summary>
        private readonly TcpListener _listener;

        #endregion

        #region 公共属性

        /// <summary>
        /// 是否已连接
        /// </summary>
        private bool IsConnected { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="ep">服务终结点</param>
        protected AsyncTcpServer(IPEndPoint ep)
            => _listener = new TcpListener(ep);

        #endregion

        #region 公共方法

        /// <summary>
        /// 启动
        /// </summary>
        public Task StartAsync()
        {
            if (IsConnected)
                return Task.CompletedTask;

            lock(_objLock)
            {
                // 启动服务器
                _listener.Start(100);
                _ = AcceptClientAsync();

                IsConnected = true;
                return Task.CompletedTask;
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public Task StopAsync()
        {
            IsConnected = false;
            _listener.Stop();
            return Task.CompletedTask;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 接收客户端
        /// </summary>
        /// <returns></returns>
        private Task AcceptClientAsync()
        {
            return Task.Run(async () =>
            {
                while (IsConnected)
                {
                    try
                    {
                        var client = await _listener.AcceptTcpClientAsync();
                        var asyncClient = new AsyncTcpClient(client);
                        //asyncClient.OnReceivedData += ClientReceivedData;
                        await asyncClient.ReceiveAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                }
            });
        }

        private void ClientReceivedData()
        {

        }

        #endregion

        #region 抽象方法

        /// <summary>
        /// 数据接收
        /// </summary>
        /// <param name="data">二进制数据</param>
        protected abstract void OnDataReceived(byte[] data);

        #endregion
    }
}
