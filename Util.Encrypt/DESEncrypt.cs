using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Util.Encrypt
{
    public static class DESEncrypt
    {
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="sInputString">需要加密的字符串</param>
        /// <param name="sKey">密钥</param>
        /// <param name="sIV"></param>
        /// <returns></returns>
        public static string EncryptString(string sInputString, string sKey, string sIV)
        {
            byte[] data = Encoding.UTF8.GetBytes(sInputString);
            using (DESCryptoServiceProvider DES = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(sKey),
                IV = Encoding.ASCII.GetBytes(sIV)
            })
            {
                ICryptoTransform desencrypt = DES.CreateEncryptor();
                byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
                return BitConverter.ToString(result);
            }
        }


        public static string DecryptString(string sInputString, string sKey, string sIV)
        {
            string[] sInput = sInputString.Split("-".ToCharArray());
            byte[] data = new byte[sInput.Length];
            for (int i = 0; i < sInput.Length; i++)
            {
                data[i] = byte.Parse(sInput[i], NumberStyles.HexNumber);
            }
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = Encoding.ASCII.GetBytes(sKey);
            DES.IV = Encoding.ASCII.GetBytes(sIV);
            ICryptoTransform desencrypt = DES.CreateDecryptor();
            byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
            return Encoding.UTF8.GetString(result);
        }
    }
}
