using Jhpj.ApiModel;
using Jhpj.Crypto;
using Jhpj.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace HttpClients.Services.JhpjGeo
{
    public class JhpjGeoClient
    {
        public static async Task Request()
        {
            await SayHelloAsync();
            await UploadStatusChangeAsync();
            await UploadStatusAsync();
        }

        private static async Task UploadStatusAsync()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Texts\\JhpjRequestStatus.json");
            //string text = "Hello World!"; 

            string text = File.ReadAllText(path);
            //var dataModel = JsonConvert.DeserializeObject<Status>(text);

            var data = AesProvider.Encrypt(text, JhpjConst.EncryptKey);
            //var dec = AesProvider.Decrypt(data, JhpjConst.EncryptKey).TrimEnd('\0');

            var sign = RsaProvider.Signature(text, JhpjConst.PrivateKey);
            //var valid = RsaProvider.Validate(sign, text, JhpjConst.PublicKey);

            var apiModel = new JhpjApiModel
            {
                AppId = JhpjConst.AppId,
                Timestamp = DateTime.Now.ToString("yyyyMMddHHmmss"),
                Data = data,
                Sign = sign,
            };

            // 发送请求
            var result = await JsonHttpRequest.PostFromJsonAsync<JhpjResultModel>(JhpjConst.StatusRoute, apiModel);
            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        }

        private static async Task UploadStatusChangeAsync()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Texts\\JhpjRequest.json");
            //string text = "Hello World!"; 
            string text = File.ReadAllText(path);
            //var dataModel = JsonConvert.DeserializeObject<Status>(text);

            var data = AesProvider.Encrypt(text, JhpjConst.EncryptKey);
            var dec = AesProvider.Decrypt(data, JhpjConst.EncryptKey).TrimEnd('\0');

            var sign = RsaProvider.Signature(text, JhpjConst.PrivateKey);
            //var valid = RsaProvider.Validate(sign, text, JhpjConst.PublicKey);

            var apiModel = new JhpjApiModel
            {
                AppId = JhpjConst.AppId,
                Timestamp = DateTime.Now.ToString("yyyyMMddHHmmss"),
                Data = data,
                Sign = sign,
            };

            // 发送请求
            var result = await JsonHttpRequest.PostFromJsonAsync<JhpjResultModel>(JhpjConst.StatusChangeRoute, apiModel);
            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        }

        private static async Task SayHelloAsync()
        {
            //var route = JhpjConst.HelloWorldRoute;
            //var result = await HttpRequest.PostAsync(route);
            //Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        }
    }
}
