using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Sockets.Extension;
using Sockets.UI;

namespace Sockets.Business
{
    public class Tcp
    {
        /// <summary>
        /// 运行Tcp连接
        /// </summary>
        public async Task RunCommandLineAsync(string[] args)
        {
            try
            {   
                var arguments = args.ToList();
                if (arguments.Count <= 0 || arguments.Contains("-h"))
                {
                    Helper();

                    return;
                }

                // 启动TcpListener
                if (arguments.Contains("-s"))
                {
                    var ipAddr = arguments.NextElement("-s");
                    var endpoint = IPEndPoint.Parse(ipAddr);
                    var capacity = 100;
                    if (arguments.Contains("-cap") && !int.TryParse(arguments.NextElement("-cap"), out capacity))
                        throw new ArgumentException("The argument of -cap command is except or mistaken", "-cap [capacity]");

                    TcpListener listener = new(endpoint);
                    listener.Start(capacity);
                    Console.WriteLine($"Tcp server has established. Capacity: {capacity.ToString()}");

                    Console.WriteLine($"Listening on {endpoint} ...");

                    while (true)
                    {
                        var client = await listener.AcceptTcpClientAsync();
                        _ = Task.Factory.StartNew(async () =>
                        {
                            Console.WriteLine($"Accepted client: {client.Client.RemoteEndPoint}");
                            byte[] buffer = new byte[1024];
                            try
                            {
                                using var stream = client.GetStream();
                                while (true)
                                {
                                    var message = await stream.ReceiveMessageAsync();
                                    if (string.IsNullOrEmpty(message)) break;

                                    await stream.SendMessageAsync(message);
                                }
                            }
                            catch (SocketException sEx)
                            {
                                Console.WriteLine(sEx);
                            }
                            finally
                            {
                                Console.WriteLine($"Disconneted client: {client.Client.RemoteEndPoint}");
                            }
                        });
                    }
                }

                // 启动TcpClient
                if (arguments.Contains("-c"))
                {
                    var ipAddr = arguments.NextElement("-c");
                    var endpoint = IPEndPoint.Parse(ipAddr);

                    TcpClient client;
                    if (arguments.Contains("-local"))
                    {
                        var localIpAddr = arguments.NextElement("-local");
                        client = new(IPEndPoint.Parse(localIpAddr));
                    }
                    else
                    {
                        client = new();
                    }
                    await client.ConnectAsync(endpoint.Address, endpoint.Port);

                    Console.WriteLine($"Client has connected to server success. Local EndPoint: {client.Client.LocalEndPoint}, Remote EndPoint: {client.Client.RemoteEndPoint}");
                    using var stream = client.GetStream();
                    _ = Task.Factory.StartNew(async () =>
                    {
                        var buffer = new byte[1024];
                        while (true)
                        {
                            try
                            {
                                var message = await stream.ReceiveMessageAsync();
                                if (string.IsNullOrEmpty(message)) break;
                            }
                            catch (SocketException cSex)
                            {
                                Console.WriteLine(cSex);
                            }
                        }
                    });
                    await stream.SendMessageAsync("Hello~!");
                    while (true)
                    {
                        Console.WriteLine($"Please input your message.(input nothing to exit~!)");
                        var message = Console.ReadLine();
                        if (string.IsNullOrEmpty(message) || !client.Connected)
                        {
                            return;
                        }
                        await stream.SendMessageAsync(message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        void Helper()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(new Unicorn().ToString());
            Console.ForegroundColor = ConsoleColor.White;

            var assemblyName = Assembly.GetExecutingAssembly().GetName();
            var commandToolName = assemblyName.Name.ToLower();
            var version = assemblyName.Version.ToString();

            Console.WriteLine();
            Console.WriteLine($"Summary");
            Console.WriteLine($"       This is a TCP connection tool");


            Console.WriteLine();
            Console.WriteLine($"Syntax");
            Console.WriteLine($"       {commandToolName} command [command arguments]");
            Console.WriteLine();
            Console.WriteLine($"Version");
            Console.WriteLine($"       {version}");
            Console.WriteLine();
            Console.WriteLine($"Command List");
            Console.WriteLine($"o  command                 description");
            Console.WriteLine($"o  -h                      Get help documents.");
            Console.WriteLine($"o  -s       [ip:port]      Set up a tcp server connection and ready for receive message or send message to any connected tcp client.");
            Console.WriteLine($"o  -cap     [capacity]     The client connection capacity for tcp server.");
            Console.WriteLine($"o  -c       [ip:port]      Set up a tcp client connection and ready for receive message and send message.");
            Console.WriteLine($"o  -local   [ip:port]      Specific a local endpoint for tcp client to bind.");
            Console.WriteLine($"o  -num     [number]       The client count for tcp client to establish and connect to tcp server.");
            // Console.WriteLine($"o  -e       [encoding]     Specific the encoding of messages");
        }
    }
}
