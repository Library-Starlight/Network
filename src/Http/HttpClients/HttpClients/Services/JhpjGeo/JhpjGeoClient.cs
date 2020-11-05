using Jhpj.ApiModel;
using Jhpj.Crypto;
using Jhpj.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.Extensions;
using System.Text;
using System.Threading.Tasks;


namespace HttpClients.Services.JhpjGeo
{
    public class JhpjGeoClient
    {
        public static async Task Request(string hostUrl, string id)
        {
            //await UploadPlainTextAsync(hostUrl);
            //return;

            await UploadStatusAsync(hostUrl, id);
            await UploadStatusChangeAsync(hostUrl, id);
        }

        private static async Task UploadPlainTextAsync(string hostUrl)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Texts\\JhpjEncryptText.json");
            string text = File.ReadAllText(path);

            //var result = await HttpRequest.PostAsync(JhpjConst.GetRoute(hostUrl, JhpjConst.StatusChangeRoute), text);
            var result = await HttpRequest.PostAsync(JhpjConst.GetRoute(hostUrl, JhpjConst.StatusChangeRoute), text);
            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        }

        private static async Task UploadStatusAsync(string hostUrl, string id)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Texts\\JhpjRequestStatus.json");
            string text = File.ReadAllText(path);

            var dataModel = JsonConvert.DeserializeObject<Status>(text);
            dataModel.serial = id;
            text = JsonConvert.SerializeObject(dataModel);

            var data = AesProvider.Encrypt(text, JhpjConst.EncryptKey.ToBytes());
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
            var result = await JsonHttpRequest.PostFromJsonAsync<JhpjResultModel>(JhpjConst.GetRoute(hostUrl, JhpjConst.StatusRoute), apiModel);
            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        }

        private static async Task UploadStatusChangeAsync(string hostUrl, string id)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Texts\\JhpjRequest.json");
            string text = File.ReadAllText(path);
            var dataModel = JsonConvert.DeserializeObject<Status>(text);
            dataModel.serial = id;
            text = JsonConvert.SerializeObject(dataModel);

            var data = AesProvider.Encrypt(text, JhpjConst.EncryptKey.ToBytes());
            var sign = RsaProvider.Signature(text, JhpjConst.PrivateKey);

            var apiModel = new JhpjApiModel
            {
                AppId = JhpjConst.AppId,
                Timestamp = DateTime.Now.ToString("yyyyMMddHHmmss"),
                Data = data,
                Sign = sign,
            };

            // 发送请求
            var result = await JsonHttpRequest.PostFromJsonAsync<JhpjResultModel>(JhpjConst.GetRoute(hostUrl, JhpjConst.StatusChangeRoute), apiModel);
            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        }
    }
}
