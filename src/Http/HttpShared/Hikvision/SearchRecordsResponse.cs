using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpShared.Hikvision
{
    public class SearchRecordsResponse : HikResponse<RecordsData>
    {
        public override List<RecordsData> data { get; set; }
    }

    public class RecordsData
    {
        public int total { get; set; }
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public List<RecordItem> list { get; set; }
    }

    public class RecordItem
    {
        public string crossRecordSyscode { get; set; }
        public string parkSyscode { get; set; }
        public string parkName { get; set; }
        public string entranceSyscode { get; set; }
        public string entranceName { get; set; }
        public string roadwaySyscode { get; set; }
        public string roadwayName { get; set; }
        public int vehicleOut { get; set; }
        public int releaseMode { get; set; }
        public int releaseResult { get; set; }
        public int releaseWay { get; set; }
        public int releaseReason { get; set; }
        public string plateNo { get; set; }
        public string cardNo { get; set; }
        public int vehicleColor { get; set; }
        public int vehicleType { get; set; }
        public int plateColor { get; set; }
        public int plateType { get; set; }
        public string carCategory { get; set; }
        public string carCategoryName { get; set; }
        public string vehiclePicUri { get; set; }
        public string plateNoPicUri { get; set; }
        public string facePicUri { get; set; }
        public string aswSyscode { get; set; }
        public DateTime crossTime { get; set; }
        public DateTime createTime { get; set; }
    }
}
