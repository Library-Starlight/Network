using HttpListener.Core.Faker;
using Jhpj.Data;
using Microsoft.Extensions.Hosting;
using System;

namespace HttpListener.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            // 加密
            //var provider = new AesProvider();
            //var text = "";

            // HttpServer
            KeyProvider.Instance.AddKey(JhpjConst.AppId, JhpjConst.PrivateKey, JhpjConst.PublicKey, JhpjConst.EncryptKey);
            new ApiHelper("http://192.168.0.179:8089").Run();

            Console.ReadLine();
        }
    }
}
