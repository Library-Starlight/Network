using System.IO;

namespace Sockets.Business.Parsers
{
    public interface IItemQuoteDecoder
    {
        ItemQuote Decode(byte[] packet);
        ItemQuote Decode(Stream stream);
    }
}
