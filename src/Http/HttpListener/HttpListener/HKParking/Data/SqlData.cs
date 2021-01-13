using HKParking.ApiModel;
using HKParking.Extensions;
using System;

namespace HKParking.Data
{
    public class SqlData
    {
        //private readonly static Database _database = new Database();

        /// <summary>
        /// 保存通行记录
        /// </summary>
        public static void SavePassRecord(PassRecordApiModel model)
        {
            var sql = SqlHelper.GenerateInsert(model, "[dbo].[HKParking_PassRecord]", "parkInfo");
            //_database.ExecuteSQL(sql.ToString());
        }

        /// <summary>
        /// 车辆是否可以通行
        /// </summary>
        /// <param name="plateNum">车牌号</param>
        /// <param name="vehType">车辆类型</param>
        /// <returns></returns>
        public static bool CanVehiclePass(string plateNum, int vehType)
        {
            var tNow = DateTime.Now;
            var sql = $"SELECT TOP 1 StartTime, EndTime FROM [dbo].[HKParking_TemporaryPlate] WHERE PlateNum = '{plateNum}' AND VehicleType = {vehType} ORDER BY CreateTime DESC";
            //var table = _database.GetDataTableFromSQL(sql);

            //if (table != null && table.Rows.Count > 0)
            //{
            //    var tStart = (DateTime)table.Rows[0]["StartTime"];
            //    var tEnd = (DateTime)table.Rows[0]["EndTime"];

            //    return tNow > tStart && tNow < tEnd;
            //}

            return false;
        }
    }
}
