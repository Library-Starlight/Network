using System.Collections.Generic;

namespace StreetLED.Model.Request
{
    /// <summary>
    /// 发布节目
    /// </summary>
    public class PublishProgramApiModel
    {
        public bool interlude { get; set; }
        public bool reset { get; set; }
        public List<int> programs { get; set; }
        public List<string> devices { get; set; }
    }
}
