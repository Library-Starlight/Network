using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace System.IO.Pipelines
{
    public class ProcessStream
    {
        async Task ProcessLinesAsync(NetworkStream stream)
        {
            var buffer = new byte[1024];

            await stream.ReadAsync(buffer, 0, buffer.Length);

            // Process a single line
            ProcessLine(buffer);
        }

        void ProcessLine(byte[] buffer)
        {

        }
    }
}
