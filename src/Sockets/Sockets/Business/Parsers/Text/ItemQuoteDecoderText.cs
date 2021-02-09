using System;
using System.IO;
using System.Text;

namespace Sockets.Business.Parsers.Text
{
    public class ItemQuoteDecoderText : IItemQuoteDecoder
    {
        private readonly byte[] _space;
        private readonly byte[] _newline;

        /// <summary>
        /// 字符串编码器
        /// </summary>
        public Encoding _encoding;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ItemQuoteDecoderText() : this(ItemQuoteTextConst.DefaultCharEnc)
        {
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ItemQuoteDecoderText(string encodingDesc)
        {
            _encoding = Encoding.GetEncoding(encodingDesc);
            _space = _encoding.GetBytes(" ");
            _newline = _encoding.GetBytes("\n");
        }

        public ItemQuote Decode(byte[] packet)
        {
            var ms = new MemoryStream(packet);
            return Decode(ms);
        }

        public ItemQuote Decode(Stream wire)
        {
            string itemNo, description, quant, price, flags;

            itemNo = _encoding.GetString(Framer.NextToken(wire, _space));
            description = _encoding.GetString(Framer.NextToken(wire, _newline));
            quant = _encoding.GetString(Framer.NextToken(wire, _space));
            price = _encoding.GetString(Framer.NextToken(wire, _space));
            flags = _encoding.GetString(Framer.NextToken(wire, _newline));

            return new ItemQuote
            {
                ItemNumber = long.Parse(itemNo),
                ItemDescription = description,
                Quantity = int.Parse(quant),
                UnitPrice = int.Parse(price),
                Discounted = flags.Contains('d'),
                InStock = flags.Contains('s'),
            };
        }
    }
}
