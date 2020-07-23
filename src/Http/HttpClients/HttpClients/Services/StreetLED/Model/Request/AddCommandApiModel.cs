using System.Collections.Generic;

namespace StreetLED.Model.Request
{
    /// <summary>
    /// 添加指令
    /// </summary>
    public class AddCommandApiModel
    {
        public string command { get; set; }
        public List<string> devices { get; set; }
    }
}
