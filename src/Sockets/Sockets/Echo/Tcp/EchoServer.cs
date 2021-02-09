using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Sockets.Echo.Tcp
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
            
            var serverStr = args[0];

            var ipEndPoint = IPEndPoint.Parse(serverStr);
            // 设置默认echo端口
            if (ipEndPoint.Port == 0) ipEndPoint.Port = 7;

            try
            {
                System.Console.WriteLine($"服务器初始化...");
                TcpListener listener = new (ipEndPoint);
                listener.Start(100);
                
                System.Console.WriteLine($"等待客户端连接...");
                while (true)
                {
                    // 等待客户端连接
                    try 
                    {
                        using var client = listener.AcceptTcpClient();
                        System.Console.WriteLine($"客户端连接上线，地址: {client.Client.RemoteEndPoint}");

                        using var stream = client.GetStream();
    
                        byte[] buffer = new byte[1024];
                        int totalRcvd = 0;
                        int rcvd = 0;
                        // 接收消息
                        while ((rcvd = stream.Read(buffer, totalRcvd, buffer.Length - totalRcvd)) != 0)
                        {
                            totalRcvd += rcvd;
                            System.Console.WriteLine($"接收{rcvd}字节消息。");
                        
                            // 发送消息
                            stream.Write(buffer, 0, totalRcvd);
                            System.Console.WriteLine($"发送{rcvd}字节消息。");
                        }

                        var message = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                        System.Console.WriteLine($"接收消息完毕，消息内容: {message}");

                        System.Console.WriteLine($"断开连接。");
                    }
                    catch (System.Exception ex0)
                    {
                        System.Console.WriteLine($"客户端异常: {ex0}");
                    }
                }
            }
            catch (SocketException sEx)
            {
                System.Console.WriteLine($"套接字异常: {sEx}");
                Environment.Exit(sEx.ErrorCode);
            }
        }
    }
}