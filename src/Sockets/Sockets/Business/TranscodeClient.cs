using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Sockets.Model;

namespace Sockets.Business
{
    public class TranscodeClient
    {
        public async Task StartAsync()
        {
            var client = new TcpClientShutdown("127.0.0.1", 8085);

            // Send
            using var stream = client.GetStream();
            using var fs = GetUtf8Stream();
            var buffer = new byte[1024 * 50];
            int count;
            while ((count = await fs.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await stream.WriteAsync(buffer, 0, count);
                System.Console.WriteLine($"Client sended {count}bytes.");
            }
            // Shutdown send channel while all data is sended
            client.Shutdown(SocketShutdown.Send);

            // Get a error if send data after shutdown the send channel
            // Ex: System.IO.IOException: Unable to write data to the transport connection: Broken pipe.
            // await stream.WriteAsync(buffer, 0, 1);

            // Rece
            using var ms = new MemoryStream();
            while ((count = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await ms.WriteAsync(buffer, 0, count);
                System.Console.WriteLine($"Client received {count}bytes.");
            }

            var unicodeData = ms.ToArray();
            // 怎么知道是Unicode编码的？
            using var fsUnicode = new FileStream($"source/longmessage_{DateTime.Now:yyyyMMddHHmmssfff}.uni", FileMode.OpenOrCreate, FileAccess.Write);
            // ms.CopyTo(fsUnicode, 1024); // ??
            await fsUnicode.WriteAsync(unicodeData, 0, unicodeData.Length);

            client.Close();
            client.Dispose();
            client = null;
        }

        public static Stream GetUtf8Stream()
            => new FileStream("source/longmessage.utf8", FileMode.Open, FileAccess.Read);
    }
}
