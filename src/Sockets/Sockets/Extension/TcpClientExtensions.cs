using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Sockets.Extension
{
    public static class TcpClientExtensions
    {
        public static async Task SendMessageAsync(this TcpClient client, string message)
        {
            System.Console.WriteLine($"Sended message: {message}");
            var data = Encoding.UTF8.GetBytes(message);
            using var stream = client.GetStream();
            await stream.WriteAsync(data, 0, data.Length);
        }
    }
}
