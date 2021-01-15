using System;

namespace Tcp.Model
{
    public class ClientReceivedDataEventArgs: EventArgs
    {
        /// <summary>
        /// 客户端
        /// </summary>
        public AsyncTcpClient Client { get; }

        /// <summary>
        /// 接收到的数据
        /// </summary>
        public byte[] Data { get; }

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ClientReceivedDataEventArgs(AsyncTcpClient client, byte[] data)
        {
            Client = client;
            Data = data;
        }

        #endregion
    }
}
