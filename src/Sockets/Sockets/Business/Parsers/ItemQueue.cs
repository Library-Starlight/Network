namespace Sockets.Business.Parsers
{
    public record ItemQuote
    {
        public long ItemNumber { get; init; }
        public string ItemDescription { get; init; }
        public int Quantity { get; set; }
        public int UnitPrice { get; init; }
        public bool Discounted { get; init; }
        public bool InStock { get; init; }
    }
}
