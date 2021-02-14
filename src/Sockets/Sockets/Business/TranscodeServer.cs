using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Sockets.Business
{
    public class TranscodeServer
    {
        public async Task StartAsync()
        {
            var server = new TcpListener(IPAddress.Loopback, 8085);
            server.Start(100);
            while (true)
            {
                var client = await server.AcceptTcpClientAsync();
                System.Console.WriteLine($"Server: new client connected.");
                _ = HandleClientAsync(client);
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            using var stream = client.GetStream();
            
            var buffer = new byte[1024 * 50];
            int count;
            var msRead = new MemoryStream();

            // Rece
            while ((count = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await msRead.WriteAsync(buffer, 0, count);
                System.Console.WriteLine($"Server received: {count}bytes.");
            }

            var message = Encoding.UTF8.GetString(msRead.ToArray());
            var data = Encoding.Unicode.GetBytes(message);
            var msWrite = new MemoryStream(data);
            // Send
            while ((count = await msWrite.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await stream.WriteAsync(buffer, 0, count);
                System.Console.WriteLine($"Server sended: {count}bytes.");
            }
        }
    }
}
