using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Sockets.Business
{
    public record ItemQueue
    {
        public long ItemNumber { get; init; }
        public string ItemDescription { get; init; }
        public int Quantity { get; init; }
        public int UnitPrice { get; init; }
        public bool Discounted { get; init; }
        public bool InStock { get; init; }
    }

    public class TcpApplicationInfrastructure
    {
        public async Task StartAsync()
        {
            var client = new TcpClient();
            try
            {
                await client.ConnectAsync(IPAddress.Loopback, 8087);
            }
            catch (SocketException ex1)
            {
                if (ex1.SocketErrorCode == SocketError.ConnectionRefused)
                    Console.WriteLine(ex1.Message);
                else
                    Console.WriteLine(ex1);
            }

            if (!client.Connected) return;

            using var stream = client.GetStream();
            await ReceiveAsync(stream);
        }

        private async Task ReceiveAsync(Stream stream)
        {
            using var sr = new StreamReader(stream, Encoding.UTF8);
            using var sw = new StreamWriter(stream, Encoding.UTF8);
            while (true)
            {
                var msg = await sr.ReadLineAsync();
                if (msg == null) break;
                Console.WriteLine(msg);
                await sw.WriteLineAsync(msg);
                await sw.FlushAsync();
            }
        }

        /// <summary>
        /// бнЪО
        /// </summary>
        /// <returns></returns>
        private Task DoSomeDemoAsync()
        {
            // Interesting
            // var s = @$"123456{'\r'}xxx{'\n'}3";
            // System.Console.WriteLine(s);

            System.Console.WriteLine(BitConverter.IsLittleEndian);
            int i = 5;

            var data = BitConverter.GetBytes(i);
            data = data.Reverse().ToArray();

            data = Encoding.Unicode.GetBytes("1");
            foreach (var b in data)
                Console.Write(b.ToString("X2"));
            System.Console.WriteLine();

            System.Console.WriteLine(Encoding.Default);
            System.Console.WriteLine(Encoding.UTF8);

            var j = IPAddress.HostToNetworkOrder(1);
            data = BitConverter.GetBytes(j);
            foreach (var b in data)
                Console.Write(b.ToString("X2"));
            System.Console.WriteLine();
            data = BitConverter.GetBytes(1);
            foreach (var b in data)
                Console.Write(b.ToString("X2"));
            System.Console.WriteLine();

            var k = IPAddress.NetworkToHostOrder(-1);
            data = BitConverter.GetBytes(k);
            foreach (var b in data)
                Console.Write(b.ToString("X2"));
            System.Console.WriteLine();
            data = BitConverter.GetBytes(-1);
            foreach (var b in data)
                Console.Write(b.ToString("X2"));
            System.Console.WriteLine();

            Encoding encoding = new UTF8Encoding(true);
            data = encoding.GetBytes("1");
            foreach (var b in data)
                Console.Write(b.ToString("X2"));
            System.Console.WriteLine();

            encoding = new UTF8Encoding(false);
            data = encoding.GetBytes("1");
            foreach (var b in data)
                Console.Write(b.ToString("X2"));
            System.Console.WriteLine();

            encoding = new UnicodeEncoding(true, false);
            data = encoding.GetBytes("1");
            foreach (var b in data)
                Console.Write(b.ToString("X2"));
            System.Console.WriteLine();

            encoding = new UnicodeEncoding(false, false);
            data = encoding.GetBytes("1");
            foreach (var b in data)
                Console.Write(b.ToString("X2"));
            System.Console.WriteLine();

            encoding = new UnicodeEncoding(true, true);
            data = encoding.GetBytes("1");
            foreach (var b in data)
                Console.Write(b.ToString("X2"));
            System.Console.WriteLine();

            encoding = new UnicodeEncoding(false, true);
            data = encoding.GetBytes("1");
            foreach (var b in data)
                Console.Write(b.ToString("X2"));
            System.Console.WriteLine();

            return Task.CompletedTask;
        }
    }
}
