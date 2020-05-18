using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients
{
    public class MD5Encrypt
    {
        /// <summary>
        /// 获取16字节（32位）大写MD5编码字符串
        /// </summary>
        /// <param name="buff">编码</param>
        /// <returns></returns>
        public static string Encrypt(byte[] buff)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(buff);
            return BitConverter.ToString(output).Replace("-", "");
        }

        public static string Encrypt(string input)
        {
            var buff = Encoding.UTF8.GetBytes(input);
            return Encrypt(buff);
        }

        public static byte[] GetEncryptData(byte[] buff)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            return md5.ComputeHash(buff);
        }

        public static byte[] GetEncryptData(string input)
        {
            var buff = Encoding.UTF8.GetBytes(input);
            return GetEncryptData(buff);
        }

        /// <summary>
        /// 获取ContentMD5数据字符串，对安全有要求的Http请求头部要求该数据
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static string GetContentMD5(string body)
        {
            var data = GetEncryptData(body);
            var base64 = new char[data.Length];

            return Convert.ToBase64String(data);
        }

        /// <summary>
        /// 获取ContentMD5数据
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static byte[] GetContentMD5Data(string body)
        {
            return Encoding.UTF8.GetBytes(GetContentMD5(body).ToLower());
        }
    }
}
