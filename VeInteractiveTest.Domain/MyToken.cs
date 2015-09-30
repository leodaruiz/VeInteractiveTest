using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VeInteractiveTest.Domain
{
    public class MyToken
    {
        private string hashKey = "k3v5j2s8d4a5p5w7o1q0i";

        public string Generate(string userId, DateTime expirationDate)
        {
            var formatedDate = string.Format("{0:ddMMyyyyHHmmss}", expirationDate);
            var textToEncrypt = userId + "-" + formatedDate;

            var encryptedText = Crypt.Encrypt(textToEncrypt, hashKey);

            return encryptedText;
        }

        public bool Validate(string password, string userIdToCheck, DateTime dateToCheck)
        {
            var decrypted = Crypt.Decrypt(password, hashKey);

            if (string.IsNullOrEmpty(decrypted))
                return false;

            var lastPos = decrypted.LastIndexOf('-');

            var userId = decrypted.Substring(0, lastPos);
            var rawDate = decrypted.Substring(lastPos + 1, decrypted.Length - (lastPos + 1));

            var expirationDate = DateTime.ParseExact(rawDate, "ddMMyyyyHHmmss", CultureInfo.InvariantCulture);

            var formatedDateToCheck = Convert.ToInt64(string.Format("{0:ddMMyyyyHHmmss}", dateToCheck));
            var formatedExpirationDate = Convert.ToInt64(string.Format("{0:ddMMyyyyHHmmss}", expirationDate));

            return formatedDateToCheck <= formatedExpirationDate && userId == userIdToCheck;
        }
    }
}
