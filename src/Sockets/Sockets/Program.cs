using System.Threading;
using Sockets.Business;
using Sockets.Business.Parsers.Binary;
using Sockets.Demo;

// new Asynchronous().StartClientAsync();

// 独角兽
// System.Console.WriteLine(new Sockets.UI.Unicorn().ToString()); 

// 演示多地址绑定
await new Demultiplexing().StartAsync();

// 演示缓冲区死锁
// new DeathLock().Start();

// Transcode Client: 演示单向关闭信道
// Transcode.Start(args);

// Tcp Echo工具
// Tcp echo client and server command line tool
// await new Sockets.Business.Tcp().RunCommandLineAsync(args);

// UdpSocket
// await new EchoSocket().RunEchoAsync(args);
// UdpClient
// new Udp().DemonstrateBroadcast();

// Udp/Tcp
// DynamicParser.Start();

// TextTcpClient Lib
//var client = new TextTcpClient();
//client.ReceivedText += async (sender, line) =>
//{
//    var c = sender as TextTcpClient;

//    // Send back
//    await c.SendAsync(line);
//};

//await client.ConnectAsync(IPAddress.Loopback, 8087);
//Console.ReadLine();

