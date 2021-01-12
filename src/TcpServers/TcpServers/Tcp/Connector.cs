using System;
using System.Collections.Generic;
using System.Text;

namespace System.Net.Sockets
{
    public abstract class Connector : Disposable, IConnector
    {
        private readonly DateTime _start = DateTime.Now;

        public event EventHandler<ReceiveEventArgs> DataReceived;
        public event EventHandler<ConnectorEventArgs> Connected;
        public event EventHandler<ConnectorEventArgs> Disconnected;

        public void Close()
        {
            Dispose(true);
        }

        public abstract void Init(params string[] args);

        public abstract int Receive(byte[] buffer, int offset, int count);

        public abstract void Send(byte[] buffer, int offset, int count);

        protected void RaiseDataReceived(object state, long sessionId, byte[] buffer, int receiveSize)
            => DataReceived?.Invoke(this, new ReceiveEventArgs(sessionId, this, state, buffer, receiveSize));

        protected void RaiseConnected(long sessionId)
            => Connected?.Invoke(this, new ConnectorEventArgs(sessionId, this));

        protected void RaiseDisconnected(long sessionId)
            => Disconnected?.Invoke(this, new ConnectorEventArgs(sessionId, this));

        public long GetNextId()
            => (long)(DateTime.Now - this._start).TotalMilliseconds;
    }
}
