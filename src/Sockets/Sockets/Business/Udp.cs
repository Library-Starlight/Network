using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sockets.Business
{
    public class Udp
    {
        public void DemonstrateBroadcast()
        {
            var t1 = new Udp().SendAsync();
            // await System.Threading.Tasks.Task.Delay(2000);
            var t2 = new Udp().ReceAsync();
            System.Threading.Tasks.Task.WhenAll(t1, t2).Wait();
        }

        private async Task SendAsync()
        {
            var client = new UdpClient();
            while (true)
            {
                var data = Encoding.ASCII.GetBytes($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Hello WOrld!");
                await client.SendAsync(data, data.Length, "255.255.255.255", 8085);
                System.Console.WriteLine("Send successed");

                await Task.Delay(500);
            }
        }

        private Task ReceAsync()
        {
            // 接收需多个ip。
            for (int i = 0; i < 1; i++)
                new Thread(async () => await ReceOneAsync()).Start();

            return Task.CompletedTask;
        }

        private static int _id = 0;
        public async Task ReceOneAsync()
        {
            var client = new UdpClient(8085);
            // System.Console.WriteLine(client.Client.LocalEndPoint);
            // System.Console.WriteLine(client.Client.RemoteEndPoint);
            var id = System.Threading.Interlocked.Increment(ref _id);

            while (true)
            {
                var result = await client.ReceiveAsync().ConfigureAwait(false);

                var message = Encoding.ASCII.GetString(result.Buffer);
                System.Console.WriteLine($"{id}: {message}");
            }
        }
    }
}
