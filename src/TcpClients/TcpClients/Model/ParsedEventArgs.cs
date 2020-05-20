using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpClients.Model
{
    public class ParsedEventArgs : EventArgs
    {
        public DataBase Data { get; set; }
    }
}
