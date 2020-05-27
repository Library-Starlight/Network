using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpShared.Hikvision
{
    public class SearchImageResponse : HikResponse<ImageData>
    {
        public override List<ImageData> data { get; set; } = null;
    }
    public class ImageData { }
}
