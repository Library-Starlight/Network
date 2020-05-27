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

        public void Send()
        {
            // 设置参数
            //HikHttpUtillib.SetPlatformInfo("28730366", "HSZkCJpSJ7gSUYrO6wVi", "10.19.132.75", 443, true);

            HikHttpUtillib.SetPlatformInfo("28730366", "HSZkCJpSJ7gSUYrO6wVi", "localhost", 5001, true);
            //HikHttpUtillib.SetPlatformInfo("28730366", "HSZkCJpSJ7gSUYrO6wVi", "localhost", 44367, true);

            SearchSpace();
            SearchRecords();
            SearchImage();
        }

        private void SearchSpace()
        {
            var request = new SearchRemainSpace { parkSyscode = "AH501 B551" };
            PostAndPrintResponse(GetSpacePath, request);
        }

        private void SearchRecords()
        {
            var request = new SearchRecords
            {
                parkSyscode = "AH501 B551"
            };
            PostAndPrintResponse(GetParkStatPath, request);
        }

        private void SearchImage()
        {
            var request = new SearchImage
            {
                aswSyscode = "hnj5h245h5234h45345y",
                picUri = "/pic?=d7ei703i10cd*73a-d5108a--22cd0c9d6592aiid=",
            };
            PostAndPrintResponse(GetPicturePath, request);
        }

        private void PostAndPrintResponse(string path, object request)
        {
            var body = JsonConvert.SerializeObject(request);

            var data = HikHttpUtillib.HttpPost(path, body, 15);

            if (null == data)
            {
                Console.WriteLine("请求失败");
            }
            else
            {
                var str = Encoding.UTF8.GetString(data);
                var jObj = JObject.Parse(str);
                Console.WriteLine(jObj.ToString(Formatting.Indented));
            }
        }
    }
}
