using System;
using System.Collections.Generic;
using System.Text;

namespace System.Net.Sockets
{
    public class Disposable : IDisposable
    {
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                }

                disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
        }
    }
}
