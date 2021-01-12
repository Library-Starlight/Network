using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Sockets.Echo.Udp
{
    public class EchoServer
    {
        public static void Start(string[] args)
        {
            if (args.Length != 1)
            {
                System.Console.WriteLine("请输入参数：<LocalIP:LocalPort>");
                return;
            }

            var localIpEndPointStr = args[0];

            var localIpEndPoint = IPEndPoint.Parse(localIpEndPointStr);
            // 设置默认echo端口
            if (localIpEndPoint.Port == 0) localIpEndPoint.Port = 7;

            UdpClient server = null;
            try
            {
                server = new(localIpEndPoint);
                System.Console.WriteLine($"初始化服务端[{server.Client.LocalEndPoint}]。");
            }
            catch (SocketException sEx0)
            {
                System.Console.WriteLine($"服务端套接字异常: {sEx0}");
                Environment.Exit(sEx0.ErrorCode);
            }

            while (true)
            {
                try
                {
                    // 接收消息
                    IPEndPoint clientIpEndPoint = new(IPAddress.Any, 0);
                    var rcvdPacket = server.Receive(ref clientIpEndPoint);
                    var message = Encoding.UTF8.GetString(rcvdPacket);
                    System.Console.WriteLine($"接收{rcvdPacket.Length}字节消息, 客户端地址: {clientIpEndPoint.ToString()}, 内容: {message}");

                    // 发送消息
                    server.Send(rcvdPacket, rcvdPacket.Length, clientIpEndPoint);
                    System.Console.WriteLine($"发送{rcvdPacket.Length}字节消息。");
                }
                catch (SocketException sEx)
                {
                    System.Console.WriteLine($"套接字异常：{sEx}");
                }
                catch (IOException iEx)
                {
                    System.Console.WriteLine($"IO异常: {iEx}");
                }
                catch (System.Exception ex)
                {
                   System.Console.WriteLine(ex);
                }
            }
        }
    }
}