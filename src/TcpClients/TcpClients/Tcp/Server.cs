using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace TcpClients.Tcp
{
    public class Server
    {
        #region 私有字段

        /// <summary>
        /// 日志类
        /// </summary>
        private ILogger _logger;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="logger"></param>
        public Server(ILogger logger)
        {
            _logger = logger;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 启动Tcp服务器
        /// </summary>
        /// <param name="port">监听的端口号</param>
        /// <returns></returns>
        public async Task StartAsync(ushort port)
        {
            var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var ep = new IPEndPoint(IPAddress.Loopback, port);
            server.Bind(ep);

            server.Listen(120);
            
            while (true)
            {
                try
                {
                    var socket = await server.AcceptAsync();

                    Console.WriteLine($"{socket.RemoteEndPoint}: 新建连接");
                    var client = new Client(socket, new Logger());
                    var task = client.HandleNetworkAsync();

                    _ = ClientMonitor(task, socket);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"服务器监听异常，{ex.ToString()}");
                    break;
                }
            }
        }

        /// <summary>
        /// 客户端监控,在客户端断开连接后,进行记录
        /// </summary>
        /// <param name="tRunning">正在运行的任务</param>
        /// <param name="socket">与客户端连接关联的套接字</param>
        /// <returns></returns>
        private async Task ClientMonitor(Task tRunning, Socket socket)
        {
            await tRunning;
            _logger.LogError($"{socket.RemoteEndPoint}: 断开连接");
        }

        #endregion

    }
}
