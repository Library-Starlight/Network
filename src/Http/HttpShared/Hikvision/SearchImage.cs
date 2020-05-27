using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpShared.Hikvision
{
    /// <summary>
    /// 查询图片Uri
    /// </summary>
    public class SearchImage
    {
        public string aswSyscode { get; set; }

        public string picUri { get; set; }
    }
}
