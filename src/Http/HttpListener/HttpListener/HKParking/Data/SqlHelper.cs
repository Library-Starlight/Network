using HKParking.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKParking.Data
{
    public class SqlHelper
    {
        /// <summary>
        /// 保存通行记录
        /// </summary>
        public static void SavePassRecord(PassRecordApiModel model)
        {

        }

        /// <summary>
        /// 车辆是否可以通行
        /// </summary>
        /// <param name="plateNum">车牌号</param>
        /// <param name="vehType">车辆类型</param>
        /// <returns></returns>
        public static bool CanVehiclePass(string plateNum, int vehType)
        {
            return true;
        }
    }
}
