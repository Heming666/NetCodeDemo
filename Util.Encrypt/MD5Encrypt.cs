using System;
using System.Security.Cryptography;
using System.Text;

namespace Util.Encrypt
{
    /// <summary>
    /// MD5加密类
    /// </summary>
    public static class MD5Encrypt
    {
        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="encryptString">要加密的字符串</param>
        /// <returns></returns>
        public static string MD5Encrypt16(string encryptString)
        {
            var md5 = new MD5CryptoServiceProvider();
            string byteStr = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(encryptString)), 4, 8).ToUpper();
            byteStr = byteStr.Replace("-", "");
            return byteStr;
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt32(string encryptString)
        {
            StringBuilder sb = new StringBuilder();
            MD5 md5 = MD5.Create(); //实例化一个md5对像
                                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(encryptString));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < bytes.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                sb.Append(bytes[i].ToString("X"));
            }
            return sb.ToString();
        }
    }
}
