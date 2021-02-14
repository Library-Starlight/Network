using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Sockets.Demo
{
    public class ClientState
    {
        public byte[] Buffer { get; }

        public NetworkStream Stream { get; }

        public StringBuilder StringBuilder { get; }

        public int TotalBytes { get; private set; }

        public ClientState(byte[] buffer, NetworkStream stream)
        {
            Buffer = buffer;
            Stream = stream;
            StringBuilder = new StringBuilder();
        }

        public void AppendResponse(string response)
        {
            StringBuilder.Append(response);
        }

        public void AddToTotalBytes(int count)
        {
            TotalBytes += count;
        }
    }

    public class Asynchronous
    {
        public static ManualResetEvent ReadDone = new ManualResetEvent(false);

        public void StartClientAsync()
        {
            var client = new TcpClient();
            client.Connect("127.0.0.1", 8085);

            var stream = client.GetStream();
            var state = new ClientState(Encoding.ASCII.GetBytes("Hello World!"), stream);

            // Write
            var result = stream.BeginWrite(state.Buffer, 0, state.Buffer.Length, WriteCallback, state);
            result.AsyncWaitHandle.WaitOne();

            // Read
            result = stream.BeginRead(state.Buffer, state.TotalBytes, state.Buffer.Length - state.TotalBytes, ReadCallback, state);
            ReadDone.WaitOne();

            stream.Close();
            stream.Dispose();
            stream = null;
            client.Close();
            client.Dispose();
            client = null;
        }

        private static void WriteCallback(IAsyncResult result)
        {
            var state = result.AsyncState as ClientState;
            state.Stream.EndWrite(result);

            System.Console.WriteLine($"Send success.");
        }

        private static void ReadCallback(IAsyncResult result)
        {
            var state = result.AsyncState as ClientState;
            var count = state.Stream.EndRead(result);
            state.AddToTotalBytes(count);

            if (state.TotalBytes < state.Buffer.Length)
            {
                state.Stream.BeginRead(state.Buffer, state.TotalBytes, state.Buffer.Length - state.TotalBytes, ReadCallback, state);
            }
            else
            {
                System.Console.WriteLine($"Rece {count}bytes. Message: {Encoding.ASCII.GetString(state.Buffer)}");
                ReadDone.Set();
            }
        }
    }
}
