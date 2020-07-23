using HttpClients;
using Newtonsoft.Json;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Http.Json
{
    public static class HttpClientExtensions1
    {
        /// <summary>
        /// 密钥Id
        /// </summary>
        private const string AppKey = "28999550";

        /// <summary>
        /// 密钥
        /// </summary>
        private const string SecretKey = "x8ds1xsd";

        public static Task<HttpResponseMessage> PostFromJsonAsync<TValue>(this HttpClient client, string url, TValue value)
        {
            var json = JsonConvert.SerializeObject(value);
            var content = new StringContent(json);

            // 请求标题头
            // Accept:*/*
            client.DefaultRequestHeaders.Add("Accept", "*/*");
            // Content-MD5: Base64(MD5(Body))
            content.Headers.ContentMD5 = MD5Encrypt.GetContentMD5Data(json);
            // Content-Type:application/json
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            // Date:yyyy-MM-dd HH:mm:ss
            //client.DefaultRequestHeaders.Add("Date", DateTime.Now.ToString());
            // X-Ca-Key:密钥ID
            client.DefaultRequestHeaders.Add("X-Ca-Key", "");
            // X-Ca-Signature:签名
            client.DefaultRequestHeaders.Add("X-Ca-Signature", Sha256.GetSignature(SecretKey, GetPresignature(client, content)));
            // X-Ca-Signature-Headers:x-ca-key
            client.DefaultRequestHeaders.Add("X-Ca-Signature-Headers", "x-ca-key");

            return client.PostAsync(url, content);
        }

        private static string GetPresignature(HttpClient client, HttpContent content)
        {
            var sb = new StringBuilder();
            sb.Append($"POST\n");
            sb.Append($"*/*\n");
            sb.Append($"{Encoding.UTF8.GetString(content.Headers.ContentMD5)}\n");
            sb.Append($"{content.Headers.ContentType}\n");
            //sb.Append($"{content.Headers.GetValues("Date").FirstOrDefault()}\n");
            sb.Append($"x-ca-key:{client.DefaultRequestHeaders.GetValues("X-Ca-Key").FirstOrDefault()}\n");
            // TODO: 替换成实际的相对Url
            sb.Append($"/artemis/api/example?a=1&b=2");

            return sb.ToString();
        }
    }
}
