using AbcClient.Internet.Abstract;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace AbcClient.Internet
{
    /// <summary>
    /// 这是一个高性能、可扩展、高可用、高并发的Tcp客户端
    /// </summary>
    public class AwesomeClient : AwesomeSocket
    {
        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="endPoint">服务器终结点</param>
        public AwesomeClient(IPEndPoint endPoint) : base(endPoint) { }

        #endregion

        #region 公共方法

        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        public override async Task StartAsync()
        {
            await Socket.ConnectAsync(EndPoint);
            await HandleDataAsync(Socket);
        }

        #endregion
    }
}
