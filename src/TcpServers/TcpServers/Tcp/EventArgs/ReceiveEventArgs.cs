using System;
using System.Collections.Generic;
using System.Text;

namespace System.Net.Sockets
{
    public class ReceiveEventArgs : ConnectorEventArgs
    {
        public int ReceiveSize { get; set; }

        public ReceiveEventArgs(long sessionId, IConnector connector) : base(sessionId, connector)
        {

        }

        public ReceiveEventArgs(long sessionId, IConnector connector, byte[] buffer) : base(sessionId, connector, buffer)
        {

        }

        public ReceiveEventArgs(long sessionId, IConnector connector, object state, byte[] buffer, int receiveSize) : base(sessionId, connector, buffer)
        {
            this.ReceiveSize = receiveSize;
            base.State = state;
        }


    }
}
