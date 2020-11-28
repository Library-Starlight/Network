using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Sockets.Echo.Tcp
{
    public class EchoClient
    {
        public static void Start(string[] args)
        {
            if (args.Length != 2) System.Console.WriteLine("请输入参数：<IP:Port> <Message>");

            var ipEndPointStr = args[0];
            var message = args[1];

            var ipEndPoint = IPEndPoint.Parse(ipEndPointStr);
            // 设置默认echo端口
            if (ipEndPoint.Port == 0) ipEndPoint.Port = 7;
            var messageBuffer = Encoding.UTF8.GetBytes(message);

            try
            {
                System.Console.WriteLine($"开始连接服务器。");
                using var client = new TcpClient(ipEndPoint.Address.ToString(), ipEndPoint.Port);
                
                System.Console.WriteLine($"准备发送消息。");
                using var stream = client.GetStream();
                
                // 发送消息
                stream.Write(messageBuffer);
                System.Console.WriteLine($"发送{messageBuffer.Length}字节到服务器...");
                System.Console.WriteLine($"消息内容: {message}");
                // 接收消息
                var totalBytesRcvd = 0;
                var bytesRcvd = 0;
                System.Console.WriteLine("准备接收消息。");
                while (totalBytesRcvd < messageBuffer.Length)
                {
                    if ((bytesRcvd = stream.Read(messageBuffer, totalBytesRcvd, messageBuffer.Length - totalBytesRcvd)) == 0)
                        break;

                    System.Console.WriteLine($"接收{bytesRcvd}字节消息。");

                    totalBytesRcvd += bytesRcvd;
                }

                System.Console.WriteLine($"结束通讯!");

                message = Encoding.UTF8.GetString(messageBuffer, 0, totalBytesRcvd);
                System.Console.WriteLine($"消息内容: {message}");
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