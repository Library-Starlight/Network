using System;
using System.Collections.Generic;
using System.Text;

namespace System.Net.Sockets
{
    public class TcpClientState
    {
        #region 公共属性

        /// <summary>
        /// 会话Id
        /// </summary>
        public long SessionId { get; set; }

        /// <summary>
        /// 与客户端关联的实例
        /// </summary>
        public TcpClient TcpClient { get; set; }

        /// <summary>
        /// 数据缓冲区
        /// </summary>
        public byte[] Buffer { get; set; }

        /// <summary>
        /// 数据流
        /// </summary>
        public NetworkStream NetworkStream
        {
            get => TcpClient.GetStream();
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="sessionId">会话Id</param>
        /// <param name="tcpClient">通讯客户端</param>
        /// <param name="buffer">缓冲区</param>
        public TcpClientState(long sessionId, TcpClient tcpClient, byte[] buffer)
        {
            if (tcpClient == null)
                throw new ArgumentException("tcpClient");
            if (buffer == null)
                throw new ArgumentException("buffer");

            this.SessionId = sessionId;
            this.TcpClient = tcpClient;
            this.Buffer = buffer;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            TcpClient.Close();
            Buffer = null;
        }

        #endregion
    }
}
