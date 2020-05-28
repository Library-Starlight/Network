using HttpShared.Hikvision;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.Services.Hikvision
{
    public class HikOpenAPI
    {
        /// <summary>
        /// 停车场编号
        /// </summary>
        private const string ParkCode = "Ah501_k551";

        /// <summary>
        /// 获取剩余停车位路径
        /// </summary>
        private const string GetSpacePath = "/api/pms/v1/park/remainSpaceNum";

        /// <summary>
        /// 获取停车记录路径
        /// </summary>
        private const string GetParkStatPath = "/api/pms/v1/crossRecords/page";

        /// <summary>
        /// 获取图片链接路径
        /// </summary>
        private const string GetPicturePath = "/api/pms/v1/image";


        #region 公共方法

        public async void Send()
        {
            Setup();

            Console.WriteLine(JsonConvert.SerializeObject(await SearchImageAsync(), Formatting.Indented));
            Console.WriteLine(JsonConvert.SerializeObject(await SearchRecordsAsync(), Formatting.Indented));
            Console.WriteLine(JsonConvert.SerializeObject(await SearchRemainSpaceAsync(), Formatting.Indented));
        }

        /// <summary>
        /// 初始化设置
        /// </summary>
        public static void Setup()
            => HikHttpUtillib.SetPlatformInfo("28730366", "HSZkCJpSJ7gSUYrO6wVi", "localhost", 5001, true);

        public async Task<SearchRecordsResponse> SearchRecordsAsync()
        {
            var request = new SearchRecords { parkSyscode = ParkCode };
            var response = await PostAsync<SearchRecordsResponse>(GetParkStatPath, request);
            return response;
        }

        public async Task<SearchImageResponse> SearchImageAsync()
        {
            var request = new SearchImage
            {
                aswSyscode = "hnj5h245h5234h45345y",
                picUri = "/pic?=d7ei703i10cd*73a-d5108a--22cd0c9d6592aiid=",
            };
            var response = await PostAsync<SearchImageResponse>(GetPicturePath, request);
            return response;
        }

        public async Task<SearchRemainSpaceResponse> SearchRemainSpaceAsync()
        {
            var request = new SearchRemainSpace { parkSyscode = ParkCode };
            var response = await PostAsync<SearchRemainSpaceResponse>(GetSpacePath, request);
            return response;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// Post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private Task<T> PostAsync<T>(string path, object request)
        {
            return Task.Run<T>(() =>
            {
                try
                {
                    var body = JsonConvert.SerializeObject(request);
                    var data = HikHttpUtillib.HttpPost(path, body, 15);

                    if (null == data)
                    {
                        Console.WriteLine("请求失败");
                        return default(T);
                    }
                    else
                    {
                        var str = Encoding.UTF8.GetString(data);
                        return JsonConvert.DeserializeObject<T>(str);
                    }
                }
                catch
                {
                    return default(T);
                }
            });
        }

        #endregion
    }
}
