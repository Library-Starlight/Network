using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pipeline
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //HttpParser

            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Loopback, 8087));
            socket.Listen(120);

            while(true)
            {
                var client = await socket.AcceptAsync();
                _ = ProcessLine(client);
            }
        }

        private static async Task ProcessLine(Socket client)
        {
            Console.WriteLine($"Client Connect: {client.LocalEndPoint}");
            // Create a network stream to process the data communication

            // Extension As Stream 
            //var stream = new NetworkStream(client);

            //await stream.ProcessSingleLineAsync();

            //await stream.ProcessMultipleLineWithAutoGrowthBufferAsync();

            //await stream.ProcessLineWithAdvancedGrowthBufferAndBufferRecycleAsync();

            // Extension As Socket
            await client.ReadAllAsync();

            Console.WriteLine($"Client Disconnect: {client.LocalEndPoint}");

        }

        #region Obsolate

        /// <summary>
        /// 将Socket包装为NetworkStream
        /// 将控制台输入流载入NetworkStream。
        /// </summary>
        /// <returns></returns>
        private static async Task ConsoleInputToSocketAsync()
        {
            var clientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Connecting to port 8087");

            clientSocket.Connect(new IPEndPoint(IPAddress.Loopback, 8087));
            var stream = new NetworkStream(clientSocket);

            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            await Console.OpenStandardInput().CopyToAsync(stream);
        }

        /// <summary>
        /// 基于Socket的Tcp协议服务器
        /// </summary>
        /// <returns></returns>
        private static async Task StartTcpServerAsync()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Loopback, 8087));

            socket.Listen(120);

            Console.WriteLine($"Listing to port 8087");
            while(true)
            {
                var client = await socket.AcceptAsync();
                _ = ProcessLinesAsync(client);
            }
        }

        private static async Task ProcessLinesAsync(Socket client)
        {
            Console.WriteLine($"Client connected: {client.LocalEndPoint}");

            var stream = new NetworkStream(client);
            var reader = PipeReader.Create(stream);

            while (true)
            {
                var result = await reader.ReadAsync();

                var buffer = result.Buffer;
                while (TryGetLine(ref buffer, out ReadOnlySequence<byte> line))
                {
                    ProcessLine(line);
                }
                if (result.IsCompleted)
                    break;

                reader.AdvanceTo(buffer.Start, buffer.End);
            }

            await reader.CompleteAsync();

            Console.WriteLine($"Client disconnected: {client.RemoteEndPoint}");
        }

        private static bool TryGetLine(ref ReadOnlySequence<byte> buffer, out ReadOnlySequence<byte> line)
        {
            var position = buffer.PositionOf((byte)'\n');
            if (!position.HasValue)
            {
                line = default;
                return false;
            }

            line = buffer.Slice(0, position.Value);
            buffer = buffer.Slice(buffer.GetPosition(1, position.Value));
            return true;
        }

        private static void ProcessLine(ReadOnlySequence<byte> buffer)
        {
            foreach (var memory in buffer)
            {
                var message = Encoding.UTF8.GetString(memory);
                Console.WriteLine(message);
            }
        }

        private static string GetSequenceLine(ReadOnlySequence<byte> buffer)
        {
            if (buffer.IsSingleSegment)
                return Encoding.ASCII.GetString(buffer.First.Span);

            return string.Create<ReadOnlySequence<byte>>((int)buffer.Length, buffer, (span, sequence) =>
            {
                foreach (var segment in sequence)
                {
                    Encoding.ASCII.GetChars(segment.Span, span);
                    span = span.Slice(segment.Length);
                }
            });
        }

        #endregion
    }
}
