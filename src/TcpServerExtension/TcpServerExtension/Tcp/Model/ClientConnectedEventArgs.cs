using System;

namespace Tcp.Model
{
    public class ClientConnectedEventArgs : EventArgs
    {
        /// <summary>
        /// Tcp客户端
        /// </summary>
        public AsyncTcpClient Client { get; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="client">客户端</param>
        public ClientConnectedEventArgs(AsyncTcpClient client)
        {
            Client = client;
        }
    }
}
