using System;
using System.IO;
using System.Net;
using System.Text;

namespace Sockets.Business.Parsers.Binary
{
    public class ItemQuoteDecoderBinary : IItemQuoteDecoder
    {
        /// <summary>
        /// 字符串编码器
        /// </summary>
        public Encoding _encoding;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ItemQuoteDecoderBinary() : this(ItemQuoteBinaryConst.DefaultCharEnc)
        {
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ItemQuoteDecoderBinary(string encodingDesc)
        {
            _encoding = Encoding.GetEncoding(encodingDesc);
        }

        public ItemQuote Decode(byte[] packet)
        {
            var ms = new MemoryStream(packet);
            return Decode(ms);
        }

        public ItemQuote Decode(Stream wire)
        {
            // DO not dispost binaryreader while Stream need to be active outside the method
            // using var br = new BinaryReader(wire);
            var br = new BinaryReader(wire);

            var itemNumber = IPAddress.NetworkToHostOrder(br.ReadInt64());
            var quantity = IPAddress.NetworkToHostOrder(br.ReadInt32());
            var unitPrice = IPAddress.NetworkToHostOrder(br.ReadInt32());
            var flags = br.ReadByte();
            
            var descLength = br.ReadByte(); // throw EndOfStreamException if there are not any data yet.
            byte[] descBuf = new byte[descLength];
            br.Read(descBuf, 0, descBuf.Length);
            var description = _encoding.GetString(descBuf);

            return new ItemQuote
            {
                ItemNumber = itemNumber,
                ItemDescription = description,
                Quantity = quantity,
                UnitPrice = unitPrice,
                Discounted = (flags & ItemQuoteBinaryConst.DiscountFlag) != 0,
                InStock = (flags & ItemQuoteBinaryConst.InStockFlag) != 0,
            };
        }
    }
}
