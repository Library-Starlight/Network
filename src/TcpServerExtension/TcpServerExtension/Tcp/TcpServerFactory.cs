using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tcp;

namespace TcpServerExtension.Implement
{
    public class TcpServerFactory : TcpServerFactoryBase<TestTcpServer>
    {
        #region 单例

        /// <summary>
        /// 线程同步锁
        /// </summary>
        private static object _objLock = new object();

        /// <summary>
        /// 单例
        /// </summary>
        private static TcpServerFactory _factory;

        /// <summary>
        /// 单例
        /// </summary>
        public static TcpServerFactory Factory
        {
            get
            {
                if (_factory == null)
                    lock (_objLock)
                        if (_factory == null)
                            _factory = new TcpServerFactory();

                return _factory;
            }
        }

        #endregion

        #region 重写方法

        /// <summary>
        /// 创建Tcp服务器
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="port">Tcp端口号</param>
        /// <returns>Tcp服务器</returns>
        protected override TestTcpServer CreateTcpServerImplementation(IPEndPoint ep)
            // 返回Tcp服务器扩展实例
            => new TestTcpServer(ep);

        #endregion
    }
}
