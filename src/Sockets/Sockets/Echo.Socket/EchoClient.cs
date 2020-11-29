using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Sockets.Echo.Socket
{
    public class EchoClient
    {
        public static void Start(string[] args)
        {
            // 参数：-socket -c -udp 192.168.0.138:50000 192.168.0.140:60001 'Hello World!'
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
            
            // 初始化套接字
            System.Net.Sockets.Socket socket = new (System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp);
            try
            {
                socket.Bind(localIpEndPoint);
                System.Console.WriteLine($"初始化套接字[{socket.LocalEndPoint}]。");
            }
            catch (SocketException sEx)
            {
                System.Console.WriteLine($"套接字异常: {sEx}");
                Environment.Exit(sEx.ErrorCode);
            }

            try 
            {
                // 发送消息
                var count = socket.SendTo(messagePacket, 0, messagePacket.Length, SocketFlags.None, remoteIpEndPoint);
                System.Console.WriteLine($"发送{count}字节。");

                // 接收消息
                EndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 0);
                // socket.DualMode = false;
                var rcvdCount = socket.ReceiveFrom(messagePacket, 0, messagePacket.Length, SocketFlags.None, ref serverEndPoint);

                System.Console.WriteLine($"接收{rcvdCount}字节。");

            }
            catch (SocketException sEx)
            {
                System.Console.WriteLine($"套接字异常: {sEx}");
            }
            catch (IOException iEx)
            {
                System.Console.WriteLine($"IO异常: {iEx}");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
            finally
            {
                socket?.Close();
            }
        }
    }
}
