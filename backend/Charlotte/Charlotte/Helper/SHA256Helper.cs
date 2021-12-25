using System.Text;
using System.Security.Cryptography;
namespace Charlotte.Helper
{
    public static class SHA256Helper
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
    }
}
