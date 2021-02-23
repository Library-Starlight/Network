using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace System.Net.Sockets
{
    /// <summary>
    /// 异步Tcp服务器
    /// </summary>
    public class AsyncTcpServer : Connector
    {
        #region 私有字段

        /// <summary>
        /// 服务器使用通讯端实例
        /// </summary>
        private TcpListener _listener;

        /// <summary>
        /// 服务器程序允许的最大客户端连接数
        /// </summary>
        private int _maxClient;

        /// <summary>
        /// 当前的连接的客户端数
        /// </summary>
        private int _clientCount;

        private bool disposed = false;

        /// <summary>
        /// 客户端会话列表
        /// </summary>
        private Dictionary<long, TcpClientState> _sessions = new Dictionary<long, TcpClientState>();

        #endregion

        #region 公共属性

        /// <summary>
        /// 服务器是否正在运行
        /// </summary>
        public bool IsRunning { get; set; }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public IPAddress Address { get; set; }

        /// <summary>
        /// 服务区端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 通信编码
        /// </summary>
        public Encoding Encoding { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="listenPort">Tcp监听端口</param>
        public AsyncTcpServer(int listenPort)
            : this(IPAddress.Any, listenPort)
        {
        }

        public AsyncTcpServer(IPEndPoint localEP)
            : this(localEP.Address, localEP.Port)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="localIPAddress">本机IP地址号</param>
        /// <param name="listenPort">Tcp监听端口</param>
        public AsyncTcpServer(IPAddress localIPAddress, int listenPort)
        {
            Address = localIPAddress;
            Port = listenPort;
            Encoding = Encoding.Default;

            _listener = new TcpListener(localIPAddress, listenPort);
            _listener.AllowNatTraversal(true);
        }

        #endregion

        /// <summary>
        /// 启动服务器
        /// </summary>
        public void Start()
        {
            if (IsRunning)
                return;

            IsRunning = true;
            _listener.Start();
            _listener.BeginAcceptTcpClient(
                new AsyncCallback(HandleTcpClientAccepted), _listener);
        }

        /// <summary>
        /// 启动服务器
        /// </summary>
        /// <param name="backlog">
        /// 服务器所允许的挂起连接序列的最大长度
        /// </param>
        public void Start(int backlog)
        {
            if (IsRunning)
                return;

            IsRunning = true;
            _listener.Start(backlog);
            _listener.BeginAcceptTcpClient(
              new AsyncCallback(HandleTcpClientAccepted), _listener);
        }

        /// <summary>
        /// 停止服务器
        /// </summary>
        public void Stop()
        {
            if (!IsRunning)
                return;

            IsRunning = false;
            _listener.Stop();
            lock (_sessions)
            {
                //关闭所有客户端连接
                CloseAllClient();
            }
        }

        /// <summary>
        /// 处理客户端连接
        /// </summary>
        /// <param name="ar"></param>
        private void HandleTcpClientAccepted(IAsyncResult ar)
        {
            if (!IsRunning)
                return;

            TcpListener listener = ar.AsyncState as TcpListener;
            try
            {
                TcpClient client = listener.EndAcceptTcpClient(ar);
                client.ReceiveTimeout = 5000;
                client.SendTimeout = 5000;

                byte[] buffer = new byte[1024];
                var sessionId = GetNextId();
                TcpClientState state = new TcpClientState(sessionId, client, buffer);
                lock (_sessions)
                {
                    _sessions.Add(sessionId, state);
                    RaiseConnected(sessionId);
                }

                NetworkStream stream = state.NetworkStream;
                // 读取数据
                stream.BeginRead(state.Buffer, 0, state.Buffer.Length, HandleDataReceived, state);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
            finally
            {
                _listener.BeginAcceptSocket(
                    new AsyncCallback(HandleTcpClientAccepted), ar.AsyncState);
            }
        }

        private void HandleDataReceived(IAsyncResult ar)
        {
            if (!IsRunning)
                return;

            TcpClientState state = (TcpClientState)ar.AsyncState;
            NetworkStream stream = state.NetworkStream;

            try
            {
                int recv = 0;
                try
                {
                    recv = stream.EndRead(ar);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.ToString());
                    recv = 0;
                }

                if (recv <= 0)
                {
                    Close(state.SessionId);

                    RaiseDisconnected(state.SessionId);
                    return;
                }

                //DEBUG
                Logger.Debug($"{state.TcpClient.Client.RemoteEndPoint} read data[{recv}]:" + string.Join(" ", state.Buffer.Take(recv).Select(x => x.ToString("X2"))));

                //触发数据收到事件
                RaiseDataReceived(state, state.SessionId, state.Buffer, recv);


                TcpClientState newState
                  = new TcpClientState(state.SessionId, state.TcpClient, new byte[1024]);
                stream.BeginRead(newState.Buffer, 0, newState.Buffer.Length, HandleDataReceived, newState);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                Close(state.SessionId);
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="state">接收数据的客户端会话</param>
        /// <param name="data">数据报文</param>
        public void Send(TcpClientState state, byte[] data)
        {
            RaisePrepareSend(state);
            Send(state.TcpClient, data);
        }

        /// <summary>
        /// 异步发送数据至指定的客户端
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="data">报文</param>
        public void Send(TcpClient client, byte[] data)
        {
            if (!IsRunning)
                throw new InvalidProgramException("This TCP Scoket server has not been started.");

            if (client == null)
                throw new ArgumentNullException("client");

            if (data == null)
                throw new ArgumentNullException("data");
            client.GetStream().BeginWrite(data, 0, data.Length, SendDataEnd, client);
        }

        /// <summary>
        /// 发送数据完成处理函数
        /// </summary>
        /// <param name="ar">目标客户端Socket</param>
        private void SendDataEnd(IAsyncResult ar)
        {
            try
            {
                ((TcpClient)ar.AsyncState).GetStream().EndWrite(ar);
                RaiseCompletedSend(null);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
        }

        #region 事件

        /// <summary>
        /// 发送数据前的事件
        /// </summary>
        public event EventHandler<AsyncEventArgs> PrepareSend;

        /// <summary>
        /// 触发发送数据前的事件
        /// </summary>
        /// <param name="state"></param>
        private void RaisePrepareSend(TcpClientState state)
        {
            if (PrepareSend != null)
            {
                PrepareSend(this, new AsyncEventArgs(state));
            }
        }

        /// <summary>
        /// 数据发送完毕事件
        /// </summary>
        public event EventHandler<AsyncEventArgs> CompletedSend;

        /// <summary>
        /// 触发数据发送完毕的事件
        /// </summary>
        /// <param name="state"></param>
        private void RaiseCompletedSend(TcpClientState state)
        {
            if (CompletedSend != null)
            {
                CompletedSend(this, new AsyncEventArgs(state));
            }
        }

        /// <summary>
        /// 网络错误事件
        /// </summary>
        public event EventHandler<AsyncEventArgs> NetError;

        /// <summary>
        /// 触发网络错误事件
        /// </summary>
        /// <param name="state"></param>
        private void RaiseNetError(TcpClientState state)
        {
            if (NetError != null)
            {
                NetError(this, new AsyncEventArgs(state));
            }
        }

        /// <summary>
        /// 异常事件
        /// </summary>
        public event EventHandler<AsyncEventArgs> OtherException;

        /// <summary>
        /// 触发异常事件
        /// </summary>
        /// <param name="state"></param>
        private void RaiseOtherException(TcpClientState state, string descrip)
        {
            if (OtherException != null)
            {
                OtherException(this, new AsyncEventArgs(descrip, state));
            }
        }
        private void RaiseOtherException(TcpClientState state)
        {
            RaiseOtherException(state, "");
        }

        #endregion

        #region Close


        public void Close(long sessionID)
        {
            lock (_sessions)
            {
                TcpClientState clientState = null;

                if (_sessions.TryGetValue(sessionID, out clientState))
                {
                    clientState.Close();
                    _sessions.Remove(sessionID);
                    _clientCount--;
                }
            }
        }
        /// <summary>
        /// 关闭所有的客户端会话,与所有的客户端连接会断开
        /// </summary>
        public void CloseAllClient()
        {
            foreach (long client in _sessions.Keys)
            {
                Close(client);
            }
            _clientCount = 0;
        }
        #endregion

        #region 释放

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release 
        /// both managed and unmanaged resources; <c>false</c> 
        /// to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    try
                    {
                        Stop();
                        _listener = null;
                    }
                    catch (SocketException)
                    {
                        RaiseOtherException(null);
                    }
                }
                disposed = true;
            }
        }

        #endregion

        public override void Init(params string[] args)
        {

        }

        public override int Receive(byte[] buffer, int index, int len)
        {
            throw new NotImplementedException();
        }

        public override void Send(byte[] buffer, int index, int len)
        {
            throw new NotImplementedException();
        }
    }
}
