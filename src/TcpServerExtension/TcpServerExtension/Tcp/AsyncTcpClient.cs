using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tcp.Model;

namespace Tcp
{
    public class AsyncTcpClient
    {
        #region 私有字段

        /// <summary>
        /// Tcp客户端
        /// </summary>
        private readonly TcpClient _client;


        /// <summary>
        /// 客户端网络数据流
        /// </summary>
        private Stream _stream { get; set; }

        /// <summary>
        /// 缓存
        /// </summary>
        private byte[] _buffer = new byte[2048];

        /// <summary>
        /// 客户端接收数据事件
        /// </summary>
        private EventHandler<ClientReceivedDataEventArgs> _clientReceivedData;

        /// <summary>
        /// 客户端断开事件
        /// </summary>
        private EventHandler<ClientDisconnectedEventArgs> _clientDisconnected;

        #endregion

        #region 公共事件

        /// <summary>
        /// 客户端接收数据事件
        /// </summary>
        public event EventHandler<ClientReceivedDataEventArgs> ClientReceivedData
        {
            add => _clientReceivedData += value;
            remove => _clientReceivedData -= value;
        }

        /// <summary>
        /// 客户端断开事件
        /// </summary>
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected
        {
            add => _clientDisconnected += value;
            remove => _clientDisconnected -= value;
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// 客户端地址
        /// </summary>
        public EndPoint RemoteEndPoint { get; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="client">Tcp客户端</param>
        public AsyncTcpClient(TcpClient client)
        {
            _client = client;
            RemoteEndPoint = _client.Client.RemoteEndPoint;
            _stream = client.GetStream();
        }

        #endregion

        #region 保护方法

        /// <summary>
        /// 触发接收数据事件
        /// </summary>
        /// <param name="args"></param>
        protected void OnClientReceivedData(ClientReceivedDataEventArgs args)
        {
            var temp = Volatile.Read(ref _clientReceivedData);
            temp?.Invoke(this, args);
        }

        /// <summary>
        /// 触发客户端断开事件
        /// </summary>
        /// <param name="args"></param>
        protected void OnClientDisconnected(ClientDisconnectedEventArgs args)
        {
            var temp = Volatile.Read(ref _clientDisconnected);
            temp?.Invoke(this, args);
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <returns></returns>
        public Task ReceiveAsync()
            => Task.Run(async () =>
            {
                while (_client.Connected)
                {
                    try
                    {
                        var count = await _stream.ReadAsync(_buffer, 0, _buffer.Length);
                        if (count == 0)
                        {
                            break;
                        }

                        var receivedData = new byte[count];
                        Array.Copy(_buffer, 0, receivedData, 0, count);

                        OnClientReceivedData(new ClientReceivedDataEventArgs(this, receivedData));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }

                Close();
            });

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">数据包</param>
        /// <returns></returns>
        public async Task SendAsync(byte[] data)
            => await _stream.WriteAsync(data, 0, data.Length);

        /// <summary>
        /// 关闭客户端连接
        /// </summary>
        /// <returns></returns>
        public void Close()
        {
            if (_client != null && _client.Connected)
            {
                _client.Close();
            }

            if (_stream != null)
            {
                _stream.Dispose();
                _stream = null;
                OnClientDisconnected(new ClientDisconnectedEventArgs(this));
            }
        }

        #endregion
    }
}