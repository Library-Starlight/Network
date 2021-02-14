using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sockets.Business
{
    public class EchoSocket
    {
        /// <summary>
        /// 运行Echo Socket(Windows)
        /// </summary>
        public Task RunEchoAsync(string[] args)
        {
            if (args.Length <= 3) return Task.CompletedTask;
            var arguments = new string[args.Length - 3];
            Array.Copy(args, 3, arguments, 0, arguments.Length);
            if (args.Contains("-socket"))
                if (args.Contains("-udp"))
                    if (args.Contains("-c"))
                        Sockets.Echo.Socket.EchoClient.Start(arguments);
                    else if (args.Contains("-normal"))
                        if (args.Contains("-udp"))
                            if (args.Contains("-c"))
                                Sockets.Echo.Udp.EchoClient.Start(arguments);
                            else if (args.Contains("-s"))
                                Sockets.Echo.Udp.EchoServer.Start(arguments);
                            else if (args.Contains("-tcp"))
                                if (args.Contains("-c"))
                                    Sockets.Echo.Tcp.EchoClient.Start(arguments);
                                else if (args.Contains("-s"))
                                    Sockets.Echo.Tcp.EchoServer.Start(arguments);
            return Task.CompletedTask;
        }

    }
}
