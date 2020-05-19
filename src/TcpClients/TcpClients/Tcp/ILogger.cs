using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpClients.Tcp
{
    public interface ILogger
    {
        void LogError(string msg);
        void LogDebug(string msg);
    }
}
