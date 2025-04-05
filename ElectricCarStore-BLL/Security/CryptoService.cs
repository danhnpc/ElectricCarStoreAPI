﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_BLL.Security
{
    public static class CryptoService
    {
        public const string SECRETKEY = "SecretKey";
        public static string AESHash(string plainText, string key, string initialVector = null)
        {

            var keyInBytes = Encoding.ASCII.GetBytes(key);

            using (var aes = Aes.Create())
            {
                aes.KeySize = keyInBytes.Length * 8; // Xác định KeySize dựa trên độ dài key
                // KeySize depends on Key length
                aes.Key = keyInBytes;
                aes.Mode = CipherMode.CBC;
                //aes.Padding = PaddingMode.PKCS7;

                if (initialVector == null)
                {
                    var initialVectorInBytes = new byte[aes.BlockSize / 8];

                    using (var rngCryptoServiceProvider = RandomNumberGenerator.Create())
                    {
                        rngCryptoServiceProvider.GetBytes(initialVectorInBytes);
                    }

                    aes.IV = initialVectorInBytes;
                }
                else
                {
                    aes.IV = Encoding.ASCII.GetBytes(initialVector);
                }

                using (var encryptor = aes.CreateEncryptor())
                {
                    using (var memStream = new MemoryStream())
                    {
                        using (var crytoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                        {
                            crytoStream.Write(aes.IV, 0, aes.IV.Length);

                            using (StreamWriter sw = new StreamWriter(crytoStream, Encoding.UTF8))
                            {
                                sw.Write(plainText);
                            }
                            var encryptedText = memStream.ToArray();

                            return Convert.ToBase64String(encryptedText);
                        }
                    }
                }
            }
        }
        public static string AESDecrypt(string encryptedText, string key)
        {
            var keyInBytes = Encoding.ASCII.GetBytes(key);
            var encryptedPasswordInBytes = Convert.FromBase64String(encryptedText);

            using (var aes = Aes.Create())
            {
                aes.Key = keyInBytes;
                //aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;

                using (var memStream = new MemoryStream(encryptedPasswordInBytes))
                {
                    var iv = new byte[aes.BlockSize / 8];
                    memStream.Read(iv, 0, iv.Length);

                    aes.IV = iv;

                    // Create an encryptor to perform the stream transform.
                    using (var decrytor = aes.CreateDecryptor())
                    {
                        using (var crytoStream = new CryptoStream(memStream, decrytor, CryptoStreamMode.Read))
                        {
                            // Here we are setting the Encoding
                            using (var sr = new StreamReader(crytoStream, Encoding.UTF8))
                            {
                                // Read all data from the stream.
                                var plainText = sr.ReadToEnd();
                                return plainText;
                            }
                        }
                    }
                }
            }
        }
        public static string SHA256Hash(string plainText)
        {
            string hashed = "";
            try
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    //string hash = GetHash(sha256Hash, source);
                    byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));

                    // Create a new Stringbuilder to collect the bytes
                    // and create a string.
                    var sBuilder = new StringBuilder();

                    // Loop through each byte of the hashed data
                    // and format each one as a hexadecimal string.
                    for (int i = 0; i < data.Length; i++)
                    {
                        sBuilder.Append(data[i].ToString("x2"));
                    }

                    // Return the hexadecimal string.
                    hashed = sBuilder.ToString();
                }

            }
            catch (Exception)
            {
                hashed = "";
            }


            return hashed;
        }

        private static byte[] GetKeyOrIv(string text, int size)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
                Array.Resize(ref data, size);
                return data;
            }
        }

        public static string RandomIV()
        {
            byte[] iv = new byte[16];

            // Sử dụng RNGCryptoServiceProvider để tạo một IV ngẫu nhiên
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(iv);
            }
            return BitConverter.ToString(iv).Replace("-", "").ToLower();

        }

        public static string Encrypt(string plainText, string secretKey, string ivKey)
        {
            byte[] key = GetKeyOrIv(secretKey, 32); // 256 bits key
            byte[] iv = GetKeyOrIv(ivKey, 16); // 128 bits IV

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText, string secretKey, string ivKey)
        {
            byte[] key = GetKeyOrIv(secretKey, 32); // 256 bits key
            byte[] iv = GetKeyOrIv(ivKey, 16); // 128 bits IV

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

    }
}
