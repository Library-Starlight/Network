using AbcClient.Internet.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AbcClient.Internet
{
    /// <summary>
    /// 这是一个高性能、可扩展、高可用、高并发的Tcp服务器
    /// </summary>
    public class AwesomeServer : AwesomeSocket
    {
        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="port">启动服务器时绑定的端口</param>
        public AwesomeServer(ushort port): base(new IPEndPoint(IPAddress.Loopback, port)) { }

        #endregion

        #region 重写方法

        public async override Task StartAsync()
        {
            Socket.Bind(EndPoint);
            Socket.Listen(60);
            
            while (true)
            {
                var client = await Socket.AcceptAsync();
                _ = ReceiveAsync(client);
            }
        }

        #endregion
    }
}
