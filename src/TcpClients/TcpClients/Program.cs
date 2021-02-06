using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TcpClients.Handler;
using TcpClients.Helper;
using TcpClients.Model;
using TcpClients.Tcp;

namespace TcpClients
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await TcpServerFramework();
        }

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
            DataPipeline.Instance.ParsedData += (s, e) =>
            {
                //ObjectHelper.GetPropertyInfo(e.Data);
                if (e.Data is DeviceData deviceData)
                {
                    ObjectHelper.GetPropertyInfo(deviceData);
                    Console.WriteLine($"{"Key",20} | {"ChannelType",20} | {"ChannelSubType",20} | {"DataType",20} | {"Value",20}");
                    foreach (var data in deviceData.Datas)
                        Console.WriteLine($"{data.Key,20} | {(int)data.Value.ChannelType,20:X2} | {data.Value.ChannelSubType,20:X2} | {data.Value.Data.GetType(),20} | {data.Value.Data,20}");
                }
            };

            var server = new Server(new Logger());
            await server.StartAsync(10077);
        }

        #endregion
    }
}
