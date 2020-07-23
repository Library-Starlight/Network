namespace StreetLED.Model
{
    public class StreetLedApiResponse<T>
    {
        public int code { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }
}
