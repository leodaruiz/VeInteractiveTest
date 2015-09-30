using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VeInteractiveTest.Domain;

namespace VeInteractiveTest.Tests
{
    [TestClass]
    public class CryptTest
    {
        [TestMethod]
        public void EncryptDecrypt_MustMatch()
        {
            //arranje
            var textToEncrypt = "VeInteractive";
            var hashKey = "k3v5j2s8d4a5p5w7o1q0i";

            //act
            var encrypted = Crypt.Encrypt(textToEncrypt, hashKey);
            var decrypted = Crypt.Decrypt(encrypted, hashKey);

            //assert
            Assert.AreEqual(textToEncrypt, decrypted);
        }
    }
}
