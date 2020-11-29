using Sockets;
using Sockets.Extension;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using static Sockets.DI.FrameworkDI;

if (args.Length <= 0) return;
var arguments = new string[args.Length - 3];
Array.Copy(args, 3, arguments, 0, arguments.Length);
if (args.Contains("-socket"))
{
    if (args.Contains("-udp"))
    {
        if (args.Contains("-c"))
        {
            Sockets.Echo.Socket.EchoClient.Start(arguments);
        }
    }
}
else if (args.Contains("-normal"))
{
    if (args.Contains("-udp"))
    {
        if (args.Contains("-c"))
        {
            Sockets.Echo.Udp.EchoClient.Start(arguments);
        }
        else if (args.Contains("-s"))
        {
            Sockets.Echo.Udp.EchoServer.Start(arguments);
        }
    }
    else if (args.Contains("-tcp"))
    {
        if (args.Contains("-c"))
        {
            Sockets.Echo.Tcp.EchoClient.Start(arguments);
        }
        else if (args.Contains("-s"))
        {
            Sockets.Echo.Tcp.EchoServer.Start(arguments);
        }
    }
}
return;

async Task RunCommandLineAsync(string[] args)
{
    try
    {
        var arguments = args.ToList();
        if (arguments.Count <= 0 || arguments.Contains("-h"))
        {
            Helper();

            return;
        }

        if (arguments.Contains("-e"))
            MessageEncoding = Encoding.GetEncoding(arguments.NextElement("-e"));
        else
            MessageEncoding = Encoding.Default;

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
            Console.WriteLine($"Tcp server has established. Encoding: {MessageEncoding.EncodingName}, Capacity: {capacity.ToString()}");

            // TODO: 接受客户端
            Console.WriteLine($"Listening on {endpoint} ...");

            Console.ReadLine();
            return;
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

            Console.ReadLine();
            return;
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
    Console.WriteLine($"o  -e       [encoding]     Specific the encoding of messages");
}

