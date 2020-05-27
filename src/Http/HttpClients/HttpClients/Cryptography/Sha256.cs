using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients
{
    public class Sha256
    {
        /// <summary>
        /// 获取Http请求签名X-Ca-Signature
        /// </summary>
        /// <param name="appSecret">密钥</param>
        /// <param name="input">待签名数据</param>
        /// <returns></returns>
        public static string GetSignature(string appSecret, string input)
        {

            return string.Empty;
        }
    }
}
