using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Util.Encrypt
{
    public static class AesEncrypt
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="inputString">加密字符串</param>
        /// <returns>加密后的数组</returns>
        public static byte[] EncryptBytes(string inputString)
        {
            using (Aes myAes = Aes.Create())
            {

                // Encrypt the string to an array of bytes.
                byte[] encrypted = Encrypt(inputString, myAes.Key, myAes.IV);
                //Display the original data and the decrypted data.
                return encrypted;
            }
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="inputString">加密字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptString(string inputString)
        {
            using (Aes myAes = Aes.Create())
            {

                // Encrypt the string to an array of bytes.
                byte[] encrypted = Encrypt(inputString, myAes.Key, myAes.IV);
                string str = Encoding.UTF8.GetString(encrypted);
                return str;
            }
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="inputString">要解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string DeCryptString(string inputString)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            using (Aes myAes = Aes.Create())
            {
                string roundtrip = Decrypt(bytes, myAes.Key, myAes.IV);
                return roundtrip;
            }
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="inputString">要解密的数组</param>
        /// <returns>解密后的字符串</returns>
        public static string DeCryptString(byte[] bytes)
        {
            using (Aes myAes = Aes.Create())
            {
                string roundtrip = Decrypt(bytes, myAes.Key, myAes.IV);
                return roundtrip;
            }
        }

        private static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        private static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}
