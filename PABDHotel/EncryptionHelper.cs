using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PABDHotel
{
    public static class EncryptionHelper
    {
        // Kunci dan IV yang sudah 100% benar ukurannya
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("12345678123456781234567812345678"); // TEPAT 32 KARAKTER
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("IniVektorUnik123");              // TEPAT 16 KARAKTER

        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            byte[] encrypted;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;
            try
            {
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (MemoryStream msDecrypt = new MemoryStream(cipherTextBytes))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return "[Data Gagal Dekripsi]";
            }
        }
    }
}
