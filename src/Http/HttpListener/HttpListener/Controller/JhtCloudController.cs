using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace HttpListener.Controller
{
    public class JhtCloudController : ApiController
    {
        /// <summary>
        /// 测试接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("JhtCloud/Test/")]
        public async Task<IHttpActionResult> Test()
        {
            await Task.Delay(5);

            return Ok();
        }

        /// <summary>
        /// 获取空闲车位信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [Route("JhtCloud/Data/queryparkspace")]
        public async Task<JhtCloudResponse<QueryParkSpaceResponse>> Get(JhtCloudRequest<QueryParkSpace> request)
        {
            var random = new Random(DateTime.Now.Millisecond);
            var restSpace = random.Next(0, 321);
            var response = new QueryParkSpaceResponse
            {
                parkCode = "10",
                parkName = "首都停车场",
                restSpace = restSpace,
                totalSpace = 320,
            };

            return await Get(request, response);
        }

        /// <summary>
        /// 获取车流量信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [Route("JhtCloud/Data/querycurrentparktraffic")]
        public async Task<JhtCloudResponse<QueryCurrentParkTrafficResponse>> Get(JhtCloudRequest<QueryCurrentParkTraffic> request)
        {
            var response = new QueryCurrentParkTrafficResponse
            {
                parkinTraffic = 40,
                parkoutTraffic = 47,
                totalTraffic = 87,
            };

            return await Get(request, response);
        }

        /// <summary>
        /// 获取进场车辆信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [Route("JhtCloud/Data/queryparkin")]
        public async Task<JhtCloudResponse<QueryParkInResponse>> Get(JhtCloudRequest<QueryParkIn> request)
        {
            var response = new JhtCloudResponseItem<QueryParkInResponse>[]
            {
                new JhtCloudResponseItem<QueryParkInResponse>
                {
                    objectId = "J1001",
                    operateType = "READ",
                    attributes = new QueryParkInResponse
                    {
                        parkCode = "v3_1",
                        parkName = "深蓝大厦停车场",
                        carNo = "闽A50051",
                        cardNo = "C100504375",
                        cardType = "临时卡",
                        inTime = DateTime.Now,
                        inEventType = "自动开闸",
                        inEquip = "主大门2号入口",
                        inOperator = "机器人1号",
                    },
                },
                new JhtCloudResponseItem<QueryParkInResponse>
                {
                    objectId = "J1002",
                    operateType = "READ",
                    attributes = new QueryParkInResponse
                    {
                        parkCode = "v3_1",
                        parkName = "深蓝大厦停车场",
                        carNo = "闽A50052",
                        cardNo = "C100504376",
                        cardType = "会员卡",
                        inTime = DateTime.Now,
                        inEventType = "自动开闸",
                        inEquip = "西北门入口",
                        inOperator = "机器人7号",
                    },
                },
                new JhtCloudResponseItem<QueryParkInResponse>
                {
                    objectId = "J1003",
                    operateType = "READ",
                    attributes = new QueryParkInResponse
                    {
                        parkCode = "v3_1",
                        parkName = "深蓝大厦停车场",
                        carNo = "闽A50053",
                        cardNo = "C100504377",
                        cardType = "临时卡",
                        inTime = DateTime.Now,
                        inEventType = "自动开闸",
                        inEquip = "主大门1号入口",
                        inOperator = "机器人1号",
                    },
                },
            };

            return await Get(request, response);
        }

        /// <summary>
        /// 获取出场车辆信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [Route("JhtCloud/Data/queryparkout")]
        public async Task<JhtCloudResponse<QueryParkOutResponse>> Get(JhtCloudRequest<QueryParkOut> request)
        {
            var response = new JhtCloudResponseItem<QueryParkOutResponse>[]
            {
                new JhtCloudResponseItem<QueryParkOutResponse>
                {
                    objectId = "X201",
                    operateType = "READ",
                    attributes = new QueryParkOutResponse
                    {
                        parkCode = "v3_1",
                        parkName = "深蓝大厦停车场",
                        cardNo = "闽A50051",
                        cardType = "临时卡",
                        outTime = DateTime.Now,
                        outEventType = "自动开闸",
                        outOperator = "收费口A",
                        payTypeName = "支付宝",
                        ysMoney = 50f,
                        yhMoney = 10f,
                        hgMoney = 0f,
                        ssMoney = 40f,
                        parkingTime = 80,
                        outPhotoUrlIds = "",
                        inTime = DateTime.Now,
                        inEquip = "机器人1号"
                    },
                },
                new JhtCloudResponseItem<QueryParkOutResponse>
                {
                    objectId = "X202",
                    operateType = "READ",
                    attributes = new QueryParkOutResponse
                    {
                        parkCode = "v3_1",
                        parkName = "深蓝大厦停车场",
                        cardNo = "闽A50052",
                        cardType = "临时卡",
                        outTime = DateTime.Now,
                        outEventType = "自动开闸",
                        outOperator = "收费口A",
                        payTypeName = "微信",
                        ysMoney = 50f,
                        yhMoney = 10f,
                        hgMoney = 0f,
                        ssMoney = 40f,
                        parkingTime = 80,
                        outPhotoUrlIds = "",
                        inTime = DateTime.Now,
                        inEquip = "机器人1号"
                    },
                },
                new JhtCloudResponseItem<QueryParkOutResponse>
                {
                    objectId = "X203",
                    operateType = "READ",
                    attributes = new QueryParkOutResponse
                    {
                        parkCode = "v3_1",
                        parkName = "深蓝大厦停车场",
                        cardNo = "闽A50053",
                        cardType = "临时卡",
                        outTime = DateTime.Now,
                        outEventType = "自动开闸",
                        outOperator = "收费口A",
                        payTypeName = "银行卡",
                        ysMoney = 50f,
                        yhMoney = 10f,
                        hgMoney = 0f,
                        ssMoney = 40f,
                        parkingTime = 80,
                        outPhotoUrlIds = "",
                        inTime = DateTime.Now,
                        inEquip = "机器人2号"
                    },
                },
            };

            return await Get(request, response);
        }

        /// <summary>
        /// 获取应答
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <param name="responseBody"></param>
        /// <returns></returns>
        private async Task<JhtCloudResponse<TResponse>> Get<TRequest, TResponse>(JhtCloudRequest<TRequest> request, TResponse responseBody)
            where TResponse : JhtCloudResponse
            where TRequest : JhtCloudRequest
        {
            // 模拟网络延时
            await Task.Delay(5);

            PrintReceEntityJson(request);

            if (request == null)
                return null;

            var response = new JhtCloudResponse<TResponse>
            {
                message = "成功",
                resultCode = 0,
                dataItems = new JhtCloudResponseItem<TResponse>[]
                {
                    new JhtCloudResponseItem<TResponse>
                    {
                        objectId = "X1001",
                        operateType = "READ",
                        attributes = responseBody,
                    },
                },
            };

            PrintSendEntityJson(response);

            return response;
        }

        /// <summary>
        /// 获取应答
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <param name="responseBody"></param>
        /// <returns></returns>
        private async Task<JhtCloudResponse<TResponse>> Get<TRequest, TResponse>(JhtCloudRequest<TRequest> request, IEnumerable<JhtCloudResponseItem<TResponse>> dataItems)
            where TResponse : JhtCloudResponse
            where TRequest : JhtCloudRequest
        {
            // 模拟网络延时
            await Task.Delay(5);

            PrintReceEntityJson(request);

            if (request == null)
                return null;

            var response = new JhtCloudResponse<TResponse>
            {
                message = "成功",
                resultCode = 0,
                dataItems = dataItems,
            };

            PrintSendEntityJson(response);

            return response;
        }

        #region 私有方法

        private void PrintReceEntityJson<T>(T request)
        {
            var json = Serialize(request);
            Console.WriteLine("====================================================================");
            Console.WriteLine($"Rece req: {json}");
        }
        private void PrintSendEntityJson<T>(T response)
        {
            var json = Serialize(response);
            Console.WriteLine($"Send req: {json}");
            Console.WriteLine("====================================================================");
        }

        private string Serialize<T>(T input)
        {
            return JsonConvert.SerializeObject(input, Formatting.Indented);
        }

        #endregion
    }
}
