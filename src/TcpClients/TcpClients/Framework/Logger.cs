using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpClients
{
    public class Logger : ILogger
    {
        public void LogDebug(string msg)
        {
            Console.WriteLine(msg);
        }

        public void LogError(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
