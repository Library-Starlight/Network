namespace StreetLED.Model.Response
{
    public class ProgramItem
    {
        public int id { get; set; }
        public string uuid { get; set; }
        public int enterpriseId { get; set; }
        public Options options { get; set; }
        public int colorModel { get; set; }
        public int ver { get; set; }
        public string name { get; set; }
        public int priority { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public float duration { get; set; }
        public int? index { get; set; }
        public string creator { get; set; }
        public string modifiedTime { get; set; }
        public int scanState { get; set; }
    }

    public class Options
    {
        public int gray { get; set; }
        public int colorModel { get; set; }
    }
}
