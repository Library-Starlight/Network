using System;
using System.IO.Pipelines;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace BulkTcpClient.Networks
{
    public class AbcTcpClient
    {
        /// <summary>
        /// 启动客户端
        /// </summary>
        /// <param name="ep"></param>
        public async Task StartAsync(IPEndPoint ep)
        {
            var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            await client.ConnectAsync(ep);

            var pipe = new Pipe();
            var tReader = GetDataAsync(client, pipe.Writer);
            var tWriter = HandleDataAsync(pipe.Reader);


            await Task.CompletedTask;
        }

        async Task GetDataAsync(Socket socket, PipeWriter writer)
        {
            const int BufferSize = 1024;

            while (true)
            {
                var memory = writer.GetMemory(BufferSize);

                await socket.ReceiveAsync(memory, SocketFlags.None);

            }

            await writer.CompleteAsync();
        }

        async Task HandleDataAsync(PipeReader reader)
        {
            while(true)
            {
                var readResult = await reader.ReadAsync();



                if (readResult.IsCompleted)
                {
                    break;
                }
            }

            await reader.CompleteAsync();
        }
    }
}
