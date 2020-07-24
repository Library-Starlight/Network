using System.Collections.Generic;

namespace StreetLED.Model
{
    /// <summary>
    /// 应答中分页的数据体
    /// </summary>
    /// <typeparam name="T">列表项类型</typeparam>
    public class StreetLedPagedModel<T> 
    {
        public int total { get; set; }
        public int pageNum { get; set; }
        public int pageSize { get; set; }
        public List<T> list { get; set; }
    }
}
