using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tcp.Utilities
{
    public static class TcpHelper
    {
        /// <summary>
        /// 解析终结点
        /// </summary>
        /// <param name="ipPort">IP端口号，例如127.0.0.1:8088</param>
        /// <param name="ep">解析的终结点</param>
        /// <returns>解析是否成功</returns>
        public static bool TryParse(string ipPort, out IPEndPoint ep)
        {
            if (ipPort == null)
            {
                ep = default;
                return false;
            }

            var parameters = ipPort.Split(':');
            if (parameters == null 
                || parameters.Length != 2
                || !IPAddress.TryParse(parameters[0], out var address)
                || !ushort.TryParse(parameters[1], out var port))
            {
                ep = default;
                return false;
            }

            ep = new IPEndPoint(address, port);
            return true;
        }
    }
}
