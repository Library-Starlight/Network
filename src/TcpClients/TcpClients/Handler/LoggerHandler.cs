using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpClients.Tcp;

namespace TcpClients.Handler
{
    public class LoggerHandler : IHandler
    {
        public void Parse(Client client, byte[] data)
        {
            // 日志
            var dataStr = BitConverter.ToString(data).Replace("-", "");
            Console.WriteLine($"{client.Socket.RemoteEndPoint}：接收数据 {dataStr}");
        }
    }
}
