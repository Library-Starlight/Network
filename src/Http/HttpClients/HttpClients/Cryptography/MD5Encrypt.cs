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
        /// <param name="buff">内容编码</param>
        /// <returns></returns>
        public static string Encrypt(byte[] buff)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(buff);
            return BitConverter.ToString(output).Replace("-", "");
        }

        /// <summary>
        /// 获取16字节（32位）大写MD5编码字符串
        /// </summary>
        /// <param name="input">内容</param>
        /// <returns></returns>
        public static string Encrypt(string input)
        {
            var buff = Encoding.UTF8.GetBytes(input);
            return Encrypt(buff);
        }

        /// <summary>
        /// 获取16字节（32位）大写MD5编码数组
        /// </summary>
        /// <param name="buff">内容编码</param>
        /// <returns></returns>
        public static byte[] GetEncryptData(byte[] buff)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            return md5.ComputeHash(buff);
        }

        /// <summary>
        /// 获取16字节（32位）大写MD5编码数组
        /// </summary>
        /// <param name="input">内容</param>
        /// <returns></returns>
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
            // MD5 bytes
            var data = GetEncryptData(body);

            // Base64 string
            return Convert.ToBase64String(data);
        }

        /// <summary>
        /// 获取ContentMD5数据
        /// </summary>
        /// <param name="body">Http请求的内容</param>
        /// <returns></returns>
        public static byte[] GetContentMD5Data(string body)
        {
            return Encoding.UTF8.GetBytes(GetContentMD5(body).ToLower());
        }

    }
}
