using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace DotNetCore.CustomHandler
{
    public static class EncryptionMechanism
    {

        /// <summary>
        ///  Set Your KEY and IV to do encyrption
        /// </summary>
        private static byte[] emkey = new byte[32];
        private static byte[] emiv = new byte[16];
        private static byte[] salt;
        public static void SetValut(string key)
        {
            byte[] bkey = Encoding.ASCII.GetBytes(key);
            byte[] defkey = Encoding.ASCII.GetBytes("rorolili.azure.com");
            using (MD5 md5 = MD5.Create())
            {
                salt = md5.ComputeHash(defkey);
                byte[] mysalt = new byte[bkey.Length + salt.Length];
                salt.CopyTo(mysalt, 0);
                bkey.CopyTo(mysalt, salt.Length);
                Buffer.BlockCopy(mysalt, 0, emkey, 0, 32);
                Buffer.BlockCopy(mysalt, 32, emiv, 0, 16);
            }
        }
        public static string AESEncryption(string source)
        {
            byte[] encrypted;

            if (String.IsNullOrWhiteSpace(source))
                throw new ArgumentNullException("source is invalid");

            if (emkey == null || emkey.Length <= 0)
                throw new ArgumentNullException("key is invalid");

            if (emiv == null || emiv.Length <= 0)
                throw new ArgumentNullException("iv is invalid");

            using (Aes aes = Aes.Create())
            {
                aes.Key = emkey;
                aes.IV = emiv;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(source);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted) ;
        }

        

        public static string AESDecryption(string source)
        {
            // Check arguments.
            if (source == null || source.Length <= 0)
                throw new ArgumentNullException("source is invalid");
            if (emkey == null || emkey.Length <= 0)
                throw new ArgumentNullException("key is invalid");
            if (emiv == null || emiv.Length <= 0)
                throw new ArgumentNullException("iv is invalid");

            var _source = Convert.FromBase64String(source);

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aes = Aes.Create())
            {
                aes.Key = emkey;
                aes.IV = emiv;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(_source))
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
