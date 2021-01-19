using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TcpServerExtension
{
    public class TcpClientFactory : Tcp.TcpClientFactoryBase<TestTcpClient>
    {
        #region 单例

        /// <summary>
        /// 线程同步锁
        /// </summary>
        private static object _objLock = new object();

        /// <summary>
        /// 单例
        /// </summary>
        private static TcpClientFactory _factory;

        /// <summary>
        /// 单例
        /// </summary>
        public static TcpClientFactory Factory
        {
            get
            {
                if (_factory == null)
                    lock (_objLock)
                        if (_factory == null)
                            _factory = new TcpClientFactory();

                return _factory;
            }
        }

        #endregion

        protected override TestTcpClient CreateTcpClientImplementation(IPEndPoint ep)
            => new TestTcpClient(ep);
    }
}
