using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.Services
{
    /// <summary>
    /// 捷慧通云平台接口协议
    /// </summary>
    public interface IJhtCloud
    {
        /// <summary>
        /// 获取停车位信息
        /// </summary>
        /// <param name="parkCodes">停车场编号</param>
        /// <returns></returns>
        Task<QueryParkSpaceResponse> GetParkingSpaceAsync(string parkCodes);

        /// <summary>
        /// 获取车流量信息
        /// </summary>
        /// <param name="parkCode">停车场编号</param>
        /// <param name="queryDate">查询日期</param>
        /// <returns></returns>
        Task<QueryCurrentParkTrafficResponse> GetTrafficFlowAsync(string parkCode, DateTime queryDate);

        /// <summary>
        /// 获取车辆进出记录
        /// </summary>
        /// <param name="parkCode">停车场编号</param>
        /// <param name="carNo">车牌号</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="pageIndex">查询页码</param>
        /// <param name="pageSize">查询记录数</param>
        /// <returns></returns>
        Task<IEnumerable<QueryParkInResponse>> GetEnterOutRecordAsync(string parkCode, string carNo, DateTime beginDate, DateTime endDate, int pageIndex, int pageSize);
    }
}
