using System;
using System.Collections.Generic;
using System.Text;

namespace System.Net.Sockets
{
    public class ConnectorEventArgs
    {
        #region 公共属性

        public IConnector Connector { get; set; }

        public bool IsHandled { get; set; }

        public byte[] Buffer { get; set; }

        public long SessionId { get; set; }

        public object State { get; set; }

        #endregion

        #region 构造函数

        public ConnectorEventArgs(long sessionId, IConnector connector)
        {
            SessionId = sessionId;
            Connector = connector;
            IsHandled = false;
        }

        public ConnectorEventArgs(long sessionId, IConnector connector, byte[] buffer)
            : this(sessionId, connector)
        {
            Buffer = buffer;
        }

        #endregion
    }
}
