using System;
using System.Text;

namespace HttpClients
{
    public class MD5Encryption
    {
        /// <summary>
        /// 获取16字节大写MD5编码字符串
        /// </summary>
        /// <param name="buff">编码</param>
        /// <returns></returns>
        public static string Encrypt(byte[] buff)
        {
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            byte[] bytesMD5 = md5Hasher.ComputeHash(buff);
            return BitConverter.ToString(bytesMD5).Replace("-", "");
        }

        public static string Encrypt(string input)
        {
            var buff = Encoding.ASCII.GetBytes(input);
            return Encrypt(buff);
        }
    }
}
