using System;
using System.Threading;
using Sockets.Business.Parsers.Binary;
using Sockets.Business.Parsers.Text;

namespace Sockets.Business
{
    public class DynamicParser
    {
        public static void Start()
        {
            // 编解码器
            // var encoder = new ItemQuoteEncoderBinary("ASCII");
            // var decoder = new ItemQuoteDecoderBinary("ASCII");
            var encoder = new ItemQuoteEncoderText("ASCII");
            var decoder = new ItemQuoteDecoderText("ASCII");
            
            // 通讯器
            var tcp = new DynamicParserTcp(decoder, encoder);
            var udp = new DynamicParserUdp(decoder, encoder);

            // new Thread(async () => await tcp.ReceAsync()).Start();
            // new Thread(async () => await tcp.SendAsync()).Start();
            new Thread(async () => await udp.ReceAsync()).Start();
            new Thread(async () => await udp.SendAsync()).Start();

        }
    }
}
