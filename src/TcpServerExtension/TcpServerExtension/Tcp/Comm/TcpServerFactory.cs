using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Net
{
    /// <summary>
    /// Tcp服务器创建工厂。
    /// 创建指定的Tcp服务器实现类
    /// 确保同一终结点的实例仅创建一次
    /// </summary>
    public abstract class TcpServerFactory
    {
        #region 私有字段

        /// <summary>
        /// 线程同步锁
        /// </summary>
        private readonly object _objLock = new object();

        /// <summary>
        /// Tcp服务器实例列表
        /// </summary>
        private readonly IDictionary<string, AsyncTcpServer> _servers = new Dictionary<string, AsyncTcpServer>();

        #endregion

        #region 公共方法

        /// <summary>
        /// 创建Tcp服务器
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="port">Tcp端口号</param>
        /// <returns>Tcp服务器</returns>
        public AsyncTcpServer CreateTcpServer(string ip, ushort port)
        {
            var address = IPAddress.Parse(ip);
            var ep = new IPEndPoint(address, port);

            return CreateTcpServer(ep);
        }

        /// <summary>
        /// 创建Tcp服务器
        /// </summary>
        /// <param name="ep">服务终结点</param>
        /// <returns>Tcp服务器</returns>
        public AsyncTcpServer CreateTcpServer(IPEndPoint ep)
        {
            var key = ep.ToString();
            if (_servers.ContainsKey(key))
                return _servers[key];
            else
            {
                lock (_objLock)
                {
                    var server = CreateTcpServerImplementation(ep);
                    _servers.Add(key, server);
                    return server;
                }
            }
        }

        #endregion

        #region 抽象方法

        /// <summary>
        /// 创建Tcp服务器
        /// </summary>
        /// <param name="ep">服务终结点</param>
        /// <returns>Tcp服务器</returns>
        protected abstract AsyncTcpServer CreateTcpServerImplementation(IPEndPoint ep);

        #endregion
    }
}
;