using HKParking.ApiModel;
using HKParking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace HKParking.Controller
{
    public class ParkingController : ApiController
    {
        #region 公共方法

        [HttpPost]
        [Route(Router.PassRecord)]
        public HKParkingResultModel PassRecord(PassRecordApiModel model)
        {
            if (model == null)
                return ModelInvalid();

            try
            {
                SqlData.SavePassRecord(model);
                return Success();
            }
            catch (Exception ex)
            {
                // Log
                return Error();
            }
        }

        [HttpPost]
        [Route(Router.CanIn)]
        public HKParkingResultModel CanIn(CanInApiModel model)
        {
            if (model == null)
                return ModelInvalid();

            try
            {
                SqlData.CanVehiclePass(model.plateNo, model.vehType);
                return Success();
            }
            catch (Exception ex)
            {
                // Log
                return Error();
            }
        }

        [HttpPost]
        [Route(Router.CanOut)]
        public HKParkingResultModel CanOut(CanOutApiModel model)
        {
            if (model == null)
                return ModelInvalid();

            try
            {
                SqlData.CanVehiclePass(model.plateNo, model.vehType);
                return Success();
            }
            catch (Exception ex)
            {
                // Log
                return Error();
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 成功
        /// </summary>
        /// <returns></returns>
        private HKParkingResultModel Success()
            => new HKParkingResultModel
            {
                code = 0,
                errDesc = "",
            };

        /// <summary>
        /// 拒绝放行
        /// </summary>
        /// <returns></returns>
        private HKParkingResultModel NotPass()
            => new HKParkingResultModel
            {
                code = 1,
                errDesc = "车辆未授权",
            };

        /// <summary>
        /// 异常
        /// </summary>
        /// <returns></returns>
        private HKParkingResultModel Error()
            => new HKParkingResultModel
            {
                code = -1,
                errDesc = "服务器处理失败",
            };

        /// <summary>
        /// 模型无效
        /// </summary>
        /// <returns></returns>
        private HKParkingResultModel ModelInvalid()
            => new HKParkingResultModel
            {
                code = -2,
                errDesc = "输入参数解析失败",
            };

        #endregion
    }
}