using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Core.Security
{
    /// <summary>
    /// DES加密：使用一个 56 位的密钥以及附加的 8 位奇偶校验位，产生最大 64 位的分组大小。
    /// 这是一个迭代的分组密码，使用称为 Feistel 的技术，其中将加密的文本块分成两半。
    /// 使用子密钥对其中一半应用循环功能，然后将输出与另一半进行“异或”运算；
    /// 接着交换这两半，这一过程会继续下去，但最后一个循环不交换。
    /// DES 使用 16 个循环，使用异或，置换，代换，移位操作四种基本运算。
    /// </summary>
    public class DES
    {
        //默认密钥
        public const string encryKey = "@SRM2021";
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string Encrypt(string encryptString, string key = encryKey)
        {

            if (string.IsNullOrEmpty(key))
            {
                key = encryKey;
            }
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] keyIV = keyBytes;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="decryptString"></param>
        /// <returns></returns>
        public static string Decrypt(string decryptString, string key = encryKey)
        {
            if (string.IsNullOrEmpty(key))
            {
                key = encryKey;
            }
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] keyIV = keyBytes;
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }
    }
}
