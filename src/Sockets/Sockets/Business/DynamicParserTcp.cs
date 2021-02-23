using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Sockets.Business.Parsers;

namespace Sockets.Business
{
    public class DynamicParserTcp
    {
        /// <summary>
        /// 解码器
        /// </summary>
        private readonly IItemQuoteDecoder _decoder;

        /// <summary>
        /// 编码器
        /// </summary>
        private readonly IItemQuoteEncoder _encoder;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DynamicParserTcp(IItemQuoteDecoder decoder, IItemQuoteEncoder encoder)
        {
            _decoder = decoder;
            _encoder = encoder;
        }

        public async Task SendAsync()
        {
            var client = new TcpClient("127.0.0.1", 8085);
            using var stream = client.GetStream();

            var quote = new ItemQuote
            {
                ItemNumber = 1235567889912L,
                ItemDescription = "5mm super market",
                Quantity = 25,
                UnitPrice = 50,
                Discounted = true,
                InStock = true,
            };

            while (true)
            {
                var data = _encoder.Encode(quote);

                await stream.WriteAsync(data, 0, data.Length);
                await Task.Delay(500);
                quote.Quantity += 1;
            }
        }

        public async Task ReceAsync()
        {
            var server = new TcpListener(IPAddress.Loopback, 8085);
            server.Start();

            while (true)
            {
                var client = await server.AcceptTcpClientAsync();
                using var stream = client.GetStream();

                // var buffer = new byte[1024];
                // stream.Read(buffer, 0, buffer.Length);
                // var item = _decoder.Decode(buffer);

                while (true)
                {
                    var item = _decoder.Decode(stream);
                    System.Console.WriteLine(item.ToString());
                }
            }
        }
    }
}
