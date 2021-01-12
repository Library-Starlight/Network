using System;
using System.Collections.Generic;
using System.Text;

namespace System.Net.Sockets
{
    public interface IConnector : IDisposable
    {
        /// <summary>
        /// 数据接收事件
        /// </summary>
        event EventHandler<ReceiveEventArgs> DataReceived;

        /// <summary>
        /// 连接事件
        /// </summary>
        event EventHandler<ConnectorEventArgs> Connected;

        /// <summary>
        /// 连接断开事件
        /// </summary>
        event EventHandler<ConnectorEventArgs> Disconnected;

        /// <summary>
        /// 初始化连接
        /// </summary>
        /// <param name="args"></param>
        void Init(params string[] args);

        /// <summary>
        /// 接受数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        int Receive(byte[] buffer, int offset, int count);

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        void Send(byte[] buffer, int offset, int count);

        /// <summary>
        /// 关闭连接
        /// </summary>
        void Close();
    }
}
