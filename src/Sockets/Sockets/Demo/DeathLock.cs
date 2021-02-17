using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Sockets.Demo
{
    public class DeathLock
    {
        public void Start()
        {
            new Thread(StartClient).Start();
            new Thread(StartServer).Start();

            Console.ReadLine();

            // Tcp连接生命周期
               // var server = new TcpListener(IPAddress.Loopback, 8085);
            // // var server = new TcpListener(8085);

            // server.Start();
            
            // // System.Console.WriteLine(server.LocalEndpoint.ToString());
            // // System.Console.WriteLine(IPAddress.Any);
            // try
            // {
            //     var client = new TcpClient();
            //     client.LingerState = new LingerOption(enable: true, seconds: 10);
            //     client.Connect(IPAddress.Loopback, 8085);
            //     System.Console.WriteLine("Connect success");

            //     client.Close();
            //     System.Console.WriteLine($"Close success.");
            // }
            // catch (SocketException ex)
            // {
            //     System.Console.WriteLine(ex.ErrorCode);
            //     System.Console.WriteLine(ex.SocketErrorCode);
            //     System.Console.WriteLine(ex);
            // }
            // return;
        }

        private void StartClient()
        {
            var buffer = new byte[2048 * 750];
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = 0xFF;

            var client = new TcpClient("127.0.0.1", 8085);

            // 修改SendQ
            // client.SendBufferSize = 2048 * 2000;
            using var stream = client.GetStream();
            stream.Write(buffer, 0, buffer.Length);

            System.Console.WriteLine("Client ended...~~~~~~~~!!!!!!!!!!!!!!!!!!!");
        }

        private void StartServer()
        {
            var buffer = new byte[2048];
            var server = new TcpListener(IPAddress.Loopback, 8085);
            server.Start();
            var client = server.AcceptTcpClient();

            using var stream = client.GetStream();
            int i = 1;
            while (true)
            {
                var count = stream.Read(buffer, 0, buffer.Length);
                if (count == 0) break;
                System.Console.WriteLine($"Server Rece: {i++.ToString()} {count.ToString()}bytes");

                Thread.Sleep(50);
            }

            System.Console.WriteLine($"Server ended...");
        }
    }
}
