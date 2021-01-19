using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Tcp
{
    /// <summary>
    /// Tcp客户端创建工厂。
    /// 创建指定的Tcp客户端
    /// 确保同一终结点的实例仅创建一次
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TcpClientFactoryBase<T>
        where T : AsyncTcpClient
    {
        #region 私有字段

        /// <summary>
        /// 线程同步锁
        /// </summary>
        private readonly object _objLock = new object();

        /// <summary>
        /// 实例列表
        /// </summary>
        private readonly IDictionary<string, T> _client = new Dictionary<string, T>();

        #endregion

        #region 公共方法

        /// <summary>
        /// 创建Tcp客户端
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="port">Tcp端口号</param>
        /// <returns>Tcp客户端</returns>
        public T CreateTcpClient(string ip, ushort port)
        {
            var address = IPAddress.Parse(ip);
            var ep = new IPEndPoint(address, port);

            return CreateTcpClient(ep);
        }

        /// <summary>
        /// 创建Tcp服务器
        /// </summary>
        /// <param name="ep">服务终结点</param>
        /// <returns>Tcp服务器</returns>
        public T CreateTcpClient(IPEndPoint ep)
        {
            var key = ep.ToString();
            if (_client.ContainsKey(key))
                return _client[key];
            else
            {
                lock (_objLock)
                {
                    var server = CreateTcpClientImplementation(ep);
                    _client.Add(key, server);
                    return server;
                }
            }
        }

        #endregion

        #region 抽象方法

        /// <summary>
        /// 创建Tcp客户端
        /// </summary>
        /// <param name="ep">服务终结点</param>
        /// <returns>Tcp客户端</returns>
        protected abstract T CreateTcpClientImplementation(IPEndPoint ep);

        #endregion
    }
}
