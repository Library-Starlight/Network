using System.IO;
using System.Text;

namespace Sockets.Business.Parsers.Text
{
    public class ItemQuoteEncoderText : IItemQuoteEncoder
    {
        /// <summary>
        /// 字符串编码器
        /// </summary>
        public Encoding _encoding;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ItemQuoteEncoderText() : this(ItemQuoteTextConst.DefaultCharEnc)
        {
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ItemQuoteEncoderText(string encodingDesc)
        {
            _encoding = Encoding.GetEncoding(encodingDesc);
        }

        /// <summary>
        /// 编码
        /// </summary>
        public byte[] Encode(ItemQuote item)
        {
            var sbEncode = new StringBuilder();
            sbEncode.Append($"{item.ItemNumber.ToString()} ");
            if (item.ItemDescription.IndexOf('\n') != -1)
                throw new IOException("Invalid description (contains newline)");
            sbEncode.Append($"{item.ItemDescription}\n{item.Quantity.ToString()} {item.UnitPrice.ToString()} ");

            if (item.Discounted)
                sbEncode.Append('d');
            if (item.InStock)
                sbEncode.Append('s');
            sbEncode.Append("\n");

            if (sbEncode.Length > ItemQuoteTextConst.MaxWireLength)
            throw new IOException("Encoded length too long");

            byte[] buf = _encoding.GetBytes(sbEncode.ToString());
            return buf;
        }
    }
}
