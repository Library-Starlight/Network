using System;
using System.Linq;
using System.Net;
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
        }
    }
}
