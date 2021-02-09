using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Sockets.Echo.Udp
{
    public class EchoClient
    {
        public static void Start(string[] args)
        {
            if (args.Length != 3) 
            {
                System.Console.WriteLine("请输入参数：<LocalIP:LocalPort> <RemoteIP:RemotePort> <Message>");
                return;
            }

            var localIpEndPointStr = args[0];
            var remoteIpEndPointStr = args[1];
            var message = args[2];

            var localIpEndPoint = IPEndPoint.Parse(localIpEndPointStr);
            var remoteIpEndPoint = IPEndPoint.Parse(remoteIpEndPointStr);
            // 设置默认echo端口
            if (remoteIpEndPoint.Port == 0) remoteIpEndPoint.Port = 7;
            var messagePacket = Encoding.UTF8.GetBytes(message);

            try
            {
                // 初始化，若指定远程终结点，则构造函数自动绑定随机的本地终结点。
                using UdpClient client = new(remoteIpEndPoint.Address.ToString(), remoteIpEndPoint.Port);
                // using UdpClient client = new();
                // using UdpClient client = new(localIpEndPoint);
                System.Console.WriteLine($"初始化客户端[{client.Client.LocalEndPoint}]。");

                // 发送消息
                var count = client.Send(messagePacket, messagePacket.Length);
                // var count = client.Send(messageData, messageData.Length, remoteIpEndPoint);
                System.Console.WriteLine($"发送{count}字节消息。");

                // 接收消息
                IPEndPoint serverIpEndPoint = new (IPAddress.Any, 0);
                var rcvdPacket = client.Receive(ref serverIpEndPoint);
                System.Console.WriteLine($"接收{rcvdPacket.Length}字节消息。");
            }
            catch (SocketException sEx)
            {
                System.Console.WriteLine($"套接字异常：{sEx}");
            }
            catch (IOException iEx)
            {
                System.Console.WriteLine($"IO异常: {iEx}");
            }
            catch (InvalidOperationException invalidEx)
            {
                System.Console.WriteLine($"无效操作: {invalidEx}");
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }
    }
}