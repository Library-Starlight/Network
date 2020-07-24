using System.Collections.Generic;

namespace StreetLED.Model.Request
{
    public class UpdateSettingsApiModel
    {
        public object settings { get; set; }

        public List<string> devices { get; set; }
    }
}
