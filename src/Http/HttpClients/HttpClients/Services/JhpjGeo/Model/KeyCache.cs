using Jhpj.Crypto;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.Extensions;
using System.Text;

namespace Jhpj.Model
{
    public class KeyCache
    {
        public string AppId { get; set; }
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public string EncryptKey { get; set; }

        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="cipher">已加密的文本</param>
        /// <returns></returns>
        public string DecryptData(string cipher)
            => AesProvider.Decrypt(cipher, EncryptKey.ToBytes());

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="sign">签名的内容</param>
        /// <param name="plainText">原始的内容</param>
        /// <returns></returns>
        public bool ValidateSign(string sign, string plainText)
            => RsaProvider.Validate(sign, plainText, PublicKey);
    }
}
