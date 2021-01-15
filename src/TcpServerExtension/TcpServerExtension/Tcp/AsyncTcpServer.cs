using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Tcp
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

        /// <summary>
        /// 客户端连接事件
        /// </summary>
        private EventHandler<Model.ClientConnectedEventArgs> _clientConnected;

        /// <summary>
        /// 客户端断开事件
        /// </summary>
        private EventHandler<Model.ClientDisconnectedEventArgs> _clientDisconnected;

        /// <summary>
        /// 客户端接受数据事件
        /// </summary>
        private EventHandler<Model.ClientReceivedDataEventArgs> _clientReceivedData;

        #endregion

        #region 公共事件

        /// <summary>
        /// 客户端连接事件
        /// </summary>
        public event EventHandler<Model.ClientConnectedEventArgs> ClientConnected
        {
            add 
                => _clientConnected += value;
            remove 
                => _clientConnected -= value;
        }

        /// <summary>
        /// 客户端断开事件
        /// </summary>
        public event EventHandler<Model.ClientDisconnectedEventArgs> ClientDisconnected
        {
            add
                => _clientDisconnected += value;
            remove
                => _clientDisconnected -= value;
        }

        /// <summary>
        /// 客户端接受数据事件
        /// </summary>
        public event EventHandler<Model.ClientReceivedDataEventArgs> ClientReceivedData
        {
            add
                => _clientReceivedData += value;
            remove
                => _clientReceivedData -= value;
        }

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
                Log.Logger.Instance.LogDebug($"服务器[{_listener.LocalEndpoint.ToString()}]启动，开始接受客户端连接。");
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

        #region 保护方法

        /// <summary>
        /// 触发客户端连接事件
        /// </summary>
        /// <param name="args"></param>
        protected void OnClientConnected(Model.ClientConnectedEventArgs args)
        {
            var temp = Volatile.Read(ref _clientConnected);
            temp?.Invoke(this, args);
        }
        
        /// <summary>
        /// 触发客户端断开事件
        /// </summary>
        /// <param name="args"></param>
        protected void OnClientDisconnected(Model.ClientDisconnectedEventArgs args)
        {
            var temp = Volatile.Read(ref _clientDisconnected);
            temp?.Invoke(this, args);
        }

        /// <summary>
        /// 触发客户端接受数据事件
        /// </summary>
        /// <param name="args"></param>
        protected void OnClientReceivedData(Model.ClientReceivedDataEventArgs args)
        {
            var temp = Volatile.Read(ref _clientReceivedData);
            temp?.Invoke(this, args);
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
                        OnClientConnected(new Model.ClientConnectedEventArgs(asyncClient));

                        asyncClient.ClientReceivedData += AsyncClient_ClientReceivedData;
                        asyncClient.ClientDisconnected += AsyncClient_ClientDisconnected;
                        _ = asyncClient.ReceiveAsync();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            });
        }

        /// <summary>
        /// 客户端断开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AsyncClient_ClientDisconnected(object _, Model.ClientDisconnectedEventArgs e)
            // 传递事件
            => OnClientDisconnected(e);

        /// <summary>
        /// 客户端接收数据
        /// </summary>
        private void AsyncClient_ClientReceivedData(object _, Model.ClientReceivedDataEventArgs e)
            // 传递事件
            => OnClientReceivedData(e);

        #endregion
    }
}
