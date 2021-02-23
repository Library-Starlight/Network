namespace Sockets.Business.Parsers
{
    public interface IItemQuoteEncoder
    {
        byte[] Encode(ItemQuote item);
    }

}
