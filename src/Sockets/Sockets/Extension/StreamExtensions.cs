using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sockets.Extension
{
    public static class StreamExtensions
    {
        public static async Task SendMessageAsync(this Stream stream, string message)
        {
            System.Console.WriteLine($"Sended message: {message}");
            var data = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(data, 0, data.Length);
        }

        public static async Task<string> ReceiveMessageAsync(this Stream stream)
        {
            var buffer = new byte[1024];
            var count = await stream.ReadAsync(buffer, 0, buffer.Length);
            if (count <= 0)
                return string.Empty;
            var message = Encoding.UTF8.GetString(buffer, 0, count);
            System.Console.WriteLine($"Received message: {message}");
            return message;
        }
    }
}
