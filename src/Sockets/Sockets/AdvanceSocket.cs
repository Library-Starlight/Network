using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

What();

DNS();

void What()
{
    Socket socket;

    TcpClient tcp;

    UdpClient udp;

    IPHostEntry hostInfo = Dns.GetHostEntry("192.168.0.138");

    Console.WriteLine($"主机：{hostInfo.HostName}");
    for (int i = 0; i < hostInfo.AddressList.Length; i++)
    {
        Console.WriteLine($"地址：{hostInfo.AddressList[i]}");
    }

    // IPAddress不可序列化
    //JsonConvert.SerializeObject(IPAddress.Loopback);
    //Console.WriteLine(JsonSerializer.Serialize(IPAddress.Loopback, new JsonSerializerOptions { WriteIndented = true }));
    //Console.WriteLine(JsonSerializer.Serialize(host, new JsonSerializerOptions { WriteIndented = true }));
}

void DNS()
{
    var i0 = Dns.GetHostEntry("192.168.0.140");
    Console.WriteLine(i0.HostName);
    var i1 = Dns.GetHostEntry("MACBOOKPRO-E8E1");
    Console.WriteLine(i1.HostName);

    var i = Dns.GetHostName();
    var j = Dns.GetHostEntry(i);

    var k = Dns.GetHostEntry("www.baidu.com");
    var k1 = Dns.GetHostEntry("baike.baidu.com");

    var l = Dns.GetHostEntry("sports.sina.com.cn");
    // SocketException
    //var l1 = Dns.GetHostEntry("https://sports.sina.com.cn");

    var m = Dns.GetHostEntry("127.0.0.1");
    var n = Dns.GetHostEntry("localhost");

}

void Udp()
{

}
