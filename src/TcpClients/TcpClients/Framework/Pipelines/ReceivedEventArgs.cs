using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpClients
{
    public class ReceivedEventArgs : EventArgs
    {
        public Client Client { get; set; }
        public byte[] Data { get; set; }
    }
}
