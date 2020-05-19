using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpClients.Tcp
{
    /// <summary>
    /// 数据解析接口
    /// </summary>
    public interface IHandler
    {
        void Parse(Client client, byte[] data);
    }
}
