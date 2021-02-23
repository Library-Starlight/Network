using System;
using System.Net;
using System.Net.Sockets;

namespace Sockets.Model
{
    /// <summary>
    /// 扩展Tcp客户端，能够进行单向关闭
    /// </summary>
    public class TcpClientShutdown : TcpClient
    {
        public TcpClientShutdown() : base() {}
        public TcpClientShutdown(IPEndPoint localEP) : base(localEP) {}
        public TcpClientShutdown(string server, int port) : base(server, port) {}
        public void Shutdown(SocketShutdown ss) => Client.Shutdown(ss);
        public EndPoint GetRemoteEndPoint() => Client.RemoteEndPoint;
    }
}
