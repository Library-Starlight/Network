using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TcpClients.Handler;
using TcpClients.Model;
using TcpClients.Tcp;

namespace TcpClients
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await TcpDataParse();
        }

        private static void Print(int j, int i)=>
            Console.WriteLine(i + j);

        #region Tcp服务器架构

        private static async Task TcpServerFramework()
        {
            DataPipeline.Instance.ReceivedData += Instance_ReceivedData;

            var server = new Server(new Logger());
            await server.StartAsync(10077);
        }


        private static void Instance_ReceivedData(object sender, ReceivedEventArgs e)
        {
            // 日志
            var dataStr = BitConverter.ToString(e.Data).Replace("-", "");
            Console.WriteLine($"{e.Client.Socket.RemoteEndPoint}：接收数据 {dataStr}");
        }

        #endregion

        #region Tcp数据解析

        private static async Task TcpDataParse()
        {
            var loggerHandler = new LoggerHandler();
            var parserHandler = new ParserHandler(new Logger());

            DataPipeline.Instance.Register(loggerHandler, parserHandler);

            var server = new Server(new Logger());
            await server.StartAsync(10077);
        }

        #endregion
    }
}
