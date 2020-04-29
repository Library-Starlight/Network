using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Pipeline
{
    class Program
    {
        static async Task Main(string[] args)
        {

        }


        /// <summary>
        /// 将Socket包装为NetworkStream；
        /// 将控制台输入流载入NetworkStream。
        /// </summary>
        /// <returns></returns>
        private async Task ConsoleInputToSocket()
        {
            var clientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Connecting to port 8087");

            clientSocket.Connect(new IPEndPoint(IPAddress.Loopback, 8087));
            var stream = new NetworkStream(clientSocket);

            await Console.OpenStandardInput().CopyToAsync(stream);
        }
    }
}
