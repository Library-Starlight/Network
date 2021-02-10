using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Sockets.Business.Parsers;

namespace Sockets.Business
{
    public class DynamicParserUdp
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
        public DynamicParserUdp(IItemQuoteDecoder decoder, IItemQuoteEncoder encoder)
        {
            _decoder = decoder;
            _encoder = encoder;
        }

        public async Task SendAsync()
        {
            var quote = new ItemQuote
            {
                ItemNumber = 1235567889912L,
                ItemDescription = "5mm super market",
                Quantity = 25,
                UnitPrice = 50,
                Discounted = true,
                InStock = true,
            };

            var client = new UdpClient();
            
            while (true)
            {
                var data = _encoder.Encode(quote);

                await client.SendAsync(data, data.Length, "127.0.0.1", 8085);

                await Task.Delay(500);
                quote.Quantity += 1;
            }
        }

        public async Task ReceAsync()
        {
            var client = new UdpClient(8085);
            var ep = new IPEndPoint(IPAddress.Loopback, 8085);

            while (true)
            {
                var result = await client.ReceiveAsync();

                var quote = _decoder.Decode(result.Buffer);
                System.Console.WriteLine(quote);
            }
        }
    }
}
