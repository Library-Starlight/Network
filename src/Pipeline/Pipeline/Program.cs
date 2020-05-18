using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Pipeline
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var tClient = ClientSocketStream();

            var tSerevr = ServerSocketStream();

            await Task.WhenAll(tSerevr);
        }

        #region InputToSocket

        private static async Task ServerSocketStream()
        {
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Loopback, 10080));

            // 准备监听
            socket.Listen(12000);

            Console.WriteLine($"开始监听：{socket.LocalEndPoint.ToString()}");

            while(true)
            {
                var client = await socket.AcceptAsync();

                _ = ProcessServerPartClient(client);
            }
        }

        private static async Task ProcessServerPartClient(Socket client)
        {
            // 创建网络流
            var stream = new NetworkStream(client);

            // 发送
            var tSend = InputToStream(stream);
            // 接收
            var tRece = OutputFromStream(stream);

            await Task.WhenAll(tSend, tRece);
        }

        #endregion


        #region OutputToSocket

        private static async Task ClientSocketStream()
        {
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(IPAddress.Loopback, 10080));

            // 创建网络流
            var stream = new NetworkStream(socket);

            // 发送
            var tSend = InputToStream(stream);
            // 接收
            var tRece = OutputFromStream(stream);

            await Task.WhenAll(tSend, tRece);
        }

        private static Task InputToStream(Stream destination)
        {
            return Console.OpenStandardInput().CopyToAsync(destination);
        }

        private static Task OutputFromStream(Stream stream)
        {
            return stream.CopyToAsync(Console.OpenStandardOutput());
        }

        #endregion

        #region CatchTaskException

        private static async void CatchTaskException()
        {
            try
            {
                // await 能捕捉到异常，不await捕捉不到
                await FrequentlyErrorTask();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Main catched: {ex.ToString()}");
            }
        }

        private static async Task FrequentlyErrorTask()
        {
            await Task.Run(() => throw new Exception("Exception from task"));

            throw new Exception("Exception from callback");
        }

        #endregion

        #region FileStream

        private static async Task OutputToFileStream()
        {
            using (var fs = new FileStream("h2o.txt", FileMode.Append, FileAccess.Write))
            {
                await Console.OpenStandardInput().CopyToAsync(fs);
            }
        }

        #endregion
    }
}
