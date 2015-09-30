using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VeInteractiveTest.Domain
{
    public class Crypt
    {
        public static string Encrypt(string message, string hash)
        {
            byte[] results = null;
            byte[] tripleDESKey = null;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripleDESAlgorithm = new TripleDESCryptoServiceProvider();
            try
            {
                tripleDESKey = hashProvider.ComputeHash(UTF8.GetBytes(hash));
                tripleDESAlgorithm.Key = tripleDESKey;
                tripleDESAlgorithm.Mode = CipherMode.ECB;
                tripleDESAlgorithm.Padding = PaddingMode.PKCS7;
                byte[] DataToEncrypt = UTF8.GetBytes(message);
                ICryptoTransform Encryptor = tripleDESAlgorithm.CreateEncryptor();
                results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                tripleDESAlgorithm.Clear();
                hashProvider.Clear();
            }
            return Convert.ToBase64String(results);
        }

        public static string Decrypt(string message, string hash)
        {
            byte[] results = null;
            byte[] tripleDESKey = null;
            TripleDESCryptoServiceProvider tripleDESAlgorithm = new TripleDESCryptoServiceProvider();
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider();
            try
            {
                tripleDESKey = hashProvider.ComputeHash(UTF8.GetBytes(hash));
                tripleDESAlgorithm.Key = tripleDESKey;
                tripleDESAlgorithm.Mode = CipherMode.ECB;
                tripleDESAlgorithm.Padding = PaddingMode.PKCS7;
                byte[] dataToDecrypt = Convert.FromBase64String(message);
                ICryptoTransform decryptor = tripleDESAlgorithm.CreateDecryptor();
                results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
            }
            catch
            {
                return "";
            }
            finally
            {
                tripleDESAlgorithm.Clear();
                hashProvider.Clear();
            }
            return UTF8.GetString(results);
        }
    }
}
