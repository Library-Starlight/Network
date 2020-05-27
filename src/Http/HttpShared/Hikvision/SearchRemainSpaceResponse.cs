using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpShared.Hikvision
{
    public class SearchRemainSpaceResponse : HikResponse<RemainSpaceData>
    {
        public override List<RemainSpaceData> data { get; set; }
    }

    public class RemainSpaceData
    {
        public string parkSyscode { get; set; }
        public string parkName { get; set; }
        public string parentParkSyscode { get; set; }
        public int totalPlace { get; set; }
        public int totalPermPlace { get; set; }
        public int totalReservePlace { get; set; }
        public int leftPlace { get; set; }
        public int leftPermPlace { get; set; }
        public int leftReservePlace { get; set; }
    }
}
