using Sockets.Business;
using System;
using System.Net;
using Tcp.Text;

await new TcpApplicationInfrastructure().StartAsync();

// Tcp echo client and server command line tool
// await new Tcp().RunCommandLineAsync(args);

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
