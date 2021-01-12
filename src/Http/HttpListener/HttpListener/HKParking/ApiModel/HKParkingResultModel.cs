using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKParking.ApiModel
{
    /// <summary>
    /// 接口结果
    /// </summary>
    public class HKParkingResultModel
    {
        /// <summary>
        /// 错误代码，
        /// 0：上传成功，非0：错误代码
        /// 0：能放行，非0：不能放行
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 不能放行或错误原因
        /// </summary>
        public string errDesc { get; set; }
    }
}
