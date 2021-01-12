using System;
using System.Net.Sockets;
using System.Text;

namespace TcpServers
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var server = new AsyncTcpServer(8050);
            //var server = new AsyncTcpServer(System.Net.IPAddress.Loopback, 8050);
            server.DataReceived += Server_DataReceived;
            server.Connected += Server_Connected;
            server.Disconnected += Server_Disconnected;
            server.Start();

            Console.ReadLine();
        }

        private static void Server_Disconnected(object sender, ConnectorEventArgs e)
        {
            Console.WriteLine("Disconnected");
        }

        private static void Server_Connected(object sender, ConnectorEventArgs e)
        {
            Console.WriteLine("Connected");
        }

        private static void Server_DataReceived(object sender, ReceiveEventArgs e)
        {
            var buffer = e.Buffer;
            var data = new byte[e.ReceiveSize];
            Array.Copy(buffer, data, e.ReceiveSize);
            var message = Encoding.GetEncoding("gb2312").GetString(data);
            Console.WriteLine(message);

            var client = (e.State as TcpClientState).TcpClient;
            var server = e.Connector as AsyncTcpServer;
            server.Send(client, data);
            //e.Connector.Send(data, 0, data.Length);
        }
    }
}
