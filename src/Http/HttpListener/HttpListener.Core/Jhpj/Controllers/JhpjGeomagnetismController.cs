using Jhpj.ApiModel;
using Jhpj.Data;
using Jhpj.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jhpj.Controllers
{
    public class JhpjGeomagnetismController : ControllerBase
    {
        #region API

        [Route("gateway/helloworld")]
        public string HelloWorld()
            => "Hello World!";

        [Route("gateway/helloworld")]
        [HttpPost]
        public ActionResult HelloWorld1()
            => Ok("Hello World! 你好，世界！");

        [Route("gateway/statuschange")]
        [HttpPost]
        public JhpjResultModel StatusUpload([FromBody] JhpjApiModel model)
        {
            try
            {
                if (!TryGetData<Status>(model, out var status, out var error))
                    return JhpjResultModel.CreateFailureModel(error);

                GeomagnetismProvider.Instance.Add(status);
                return JhpjResultModel.CreateSuccessModel();
            }
            catch
            {
                return JhpjResultModel.CreateFailureModel("服务器内部异常");
            }
        }

        [Route("gateway/status")]
        [HttpPost]
        public JhpjResultModel InfoUpload([FromBody] JhpjApiModel model)
        {
            try
            {
                if (!TryGetData<Info>(model, out var info, out var error))
                    return JhpjResultModel.CreateFailureModel(error);

                GeomagnetismProvider.Instance.Add(info);
                return JhpjResultModel.CreateSuccessModel();
            }
            catch 
            {
                return JhpjResultModel.CreateFailureModel("服务器内部异常");
            }
        }

        #endregion

        #region 帮助方法

        /// <summary>
        /// 获取数据体
        /// </summary>
        /// <typeparam name="T">数据体类型</typeparam>
        /// <param name="model">请求数据</param>
        /// <param name="data">数据体</param>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        private bool TryGetData<T>(JhpjApiModel model, out T data, out string error)
        {
            try
            {
                var keyCache = KeyProvider.Instance.GetKey(model.AppId);

                if (keyCache == null)
                {
                    data = default;
                    error = "app_id无效";
                    return false;
                }

                var plainText = keyCache.DecryptData(model.Data).TrimEnd('\0');
                if (!keyCache.ValidateSign(model.Sign, plainText))
                {
                    data = default;
                    error = "签名校验失败";
                    return false;
                }

                data = JsonConvert.DeserializeObject<T>(plainText);
                error = default;
                return true;
            }
            catch
            {
                data = default;
                error = "服务器内部异常";
                return false;
            }
        }

        #endregion
    }
}
