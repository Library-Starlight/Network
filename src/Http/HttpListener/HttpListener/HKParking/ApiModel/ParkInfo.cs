using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKParking.ApiModel
{
    /// <summary>
    /// 停车场信息
    /// </summary>
    public class Parkinfo
    {
        public string regionName { get; set; }
        public int regionId { get; set; }
        public int totalLots { get; set; }
        public int currentLots { get; set; }
    }
}
