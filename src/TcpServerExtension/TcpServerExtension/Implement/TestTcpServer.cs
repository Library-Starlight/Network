using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TcpServerExtension.Implement
{
    /// <summary>
    /// Tcp服务器，将二进制数据为易用的实体类
    /// </summary>
    public class TestTcpServer : AsyncTcpServer
    {
        public TestTcpServer(IPEndPoint ep)
            : base(ep)
        {

        }

        protected override void OnDataReceived(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
