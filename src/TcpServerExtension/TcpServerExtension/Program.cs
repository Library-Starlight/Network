﻿using System;
using System.Net;
using System.Threading.Tasks;
using Tcp.Model;
using Tcp.Utilities;
using TcpServerExtension.Implement;

namespace TcpServerExtension
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var addr = "127.0.0.1:18088";

            TcpHelper.TryParse(addr, out var ep);
            var server = TcpServerFactory.Factory.CreateTcpServer(ep);
            await server.StartAsync();

            Console.WriteLine($"按Enter退出！");
            Console.ReadLine();
        }
    }
}
