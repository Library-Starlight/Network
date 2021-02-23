using System;

namespace Sockets.Business.Parsers.Binary
{
    public static class ItemQuoteBinaryConst
    {
        public static readonly string DefaultCharEnc = "ascii";
        public static readonly byte DiscountFlag = 1 << 7;
        public static readonly byte InStockFlag = 1 << 0;
        public static readonly int MaxDescLength = 255;
        public static readonly int MaxWireLength = 1024;
    }
}
