using System;
using System.IO;
using System.Net;
using System.Text;

namespace Sockets.Business.Parsers.Binary
{
    public class ItemQuoteEncoderBinary : IItemQuoteEncoder
    {
        /// <summary>
        /// 字符串编码器
        /// </summary>
        public Encoding _encoding;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ItemQuoteEncoderBinary() : this(ItemQuoteBinaryConst.DefaultCharEnc)
        {
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ItemQuoteEncoderBinary(string encodingDesc)
        {
            _encoding = Encoding.GetEncoding(encodingDesc);
        }

        /// <summary>
        /// 编码
        /// </summary>
        public byte[] Encode(ItemQuote item)
        {
            using var ms = new MemoryStream();
            using var output = new BinaryWriter(new BufferedStream(ms));

            output.Write(IPAddress.HostToNetworkOrder(item.ItemNumber));
            output.Write(IPAddress.HostToNetworkOrder(item.Quantity));
            output.Write(IPAddress.HostToNetworkOrder(item.UnitPrice));

            byte flags = 0;
            if (item.Discounted)
                flags |= ItemQuoteBinaryConst.DiscountFlag;
            if (item.InStock)
                flags |= ItemQuoteBinaryConst.InStockFlag;
            output.Write(flags);

            var encodeDesc = _encoding.GetBytes(item.ItemDescription);
            if (encodeDesc.Length > ItemQuoteBinaryConst.MaxDescLength)
                throw new IOException("Item Description exceeds encoded length limit");
            output.Write((byte)encodeDesc.Length);
            output.Write(encodeDesc);

            output.Flush();

            return ms.ToArray();
        }
    }
}
