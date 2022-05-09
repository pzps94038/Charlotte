using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Charlotte.Services
{
    public class EncryptHelper
    {

        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string SHA256Encrypt(string text)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(text);
            var result = SHA256.Create().ComputeHash(plainBytes);
            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="plainText">資料文字</param>
        /// <param name="key">key</param>
        /// <param name="iv">iv</param>
        /// <returns>加密後文字</returns>
        /// <exception cref="Exception"></exception>
        public static string AESEncrypt(string plainText, string key, string iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            swEncrypt.Write(plainText);
                        string? encrypt = msEncrypt.ToString();
                        if (encrypt != null)
                            return encrypt;
                        else
                            throw new Exception("加密失敗");
                    }
                }
            }
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="cipherText">資料文字</param>
        /// <param name="key">key</param>
        /// <param name="iv">iv</param>
        /// <returns></returns>
        public static string AESDecrypt(string cipherText, string key, string iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                var cipherByte = Encoding.UTF8.GetBytes(cipherText);
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(cipherByte))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }
}
