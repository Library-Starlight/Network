using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace System.Security.Cryptography.Extensions
{
    public class AesProvider
    {
        public static string Encrypt(string plainText, byte[] encryptKey)
        {
            //分组加密算法  
             SymmetricAlgorithm des = Rijndael.Create();

            var inputdata = Encoding.UTF8.GetBytes(plainText);
             byte[] inputByteArray = inputdata;//得到需要加密的字节数组      
             //设置密钥及密钥向量
             des.Key = encryptKey;
             des.IV = new byte[16];
             using (MemoryStream ms = new MemoryStream())
             {
                 using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                 {
                     cs.Write(inputByteArray, 0, inputByteArray.Length);
                     cs.FlushFinalBlock();
                     byte[] cipherBytes = ms.ToArray();//得到加密后的字节数组  
                     cs.Close();
                     ms.Close();                  
                     return Convert.ToBase64String(cipherBytes);
                 }
             }
        }

        public static string Decrypt(string cipher, byte[] encryptKey)
        {
             SymmetricAlgorithm des = Rijndael.Create();
             des.Key = encryptKey;
             des.IV = new byte[16];

            var inputData = Convert.FromBase64String(cipher);
             byte[] decryptBytes = new byte[inputData.Length];
             using (MemoryStream ms = new MemoryStream(inputData))
             {
                 using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
                 {
                     cs.Read(decryptBytes, 0, decryptBytes.Length);
                     cs.Close();
                     ms.Close();
                 }
             }

            return Encoding.UTF8.GetString(decryptBytes);
        }
    }
}
