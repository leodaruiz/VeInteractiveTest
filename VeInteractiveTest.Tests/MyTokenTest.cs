using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VeInteractiveTest.Domain;

namespace VeInteractiveTest.Tests
{
    [TestClass]
    public class MyTokenTest
    {
        [TestMethod]
        public void GeneratePassword_CanNotBeEmpty()
        {
            //arranje
            var myToken = new MyToken();

            //act
            var password = myToken.Generate("Leo", DateTime.Now);

            //assert
            Assert.IsTrue(!string.IsNullOrEmpty(password));
        }

        [TestMethod]
        public void GeneratePassword_BlankUserId_MustReturnInvalid()
        {
            //arranje
            var myToken = new MyToken();
            var expirationDate = DateTime.Now;
            var userId = "anyUserId";
            var password = myToken.Generate(userId, expirationDate);

            //act
            bool passValid = myToken.Validate(password, "", expirationDate);

            //assert
            Assert.IsFalse(passValid);
        }

        [TestMethod]
        public void GeneratePassword_BlankPassword_MustReturnInvalid()
        {
            //arranje
            var myToken = new MyToken();
            var expirationDate = DateTime.Now;
            var password = "";

            //act
            bool passValid = myToken.Validate(password, "anyUser", expirationDate);

            //assert
            Assert.IsFalse(passValid);
        }

        [TestMethod]
        public void GeneratePassword_MustBeInvalidForDifferentUser()
        {
            //arranje
            var myToken = new MyToken();
            var userId = "Leo";
            var expirationDate = DateTime.Now;
            var password = myToken.Generate(userId, expirationDate);

            //act
            bool passValid = myToken.Validate(password, "otherUser", expirationDate);

            //assert
            Assert.IsFalse(passValid);
        }

        [TestMethod]
        public void GeneratePassword_MustBeValidForUserBeforeExpiration()
        {
            //arranje
            var myToken = new MyToken();
            var userId = "Leo";
            var expirationDate = DateTime.Now;
            var password = myToken.Generate(userId, expirationDate);

            //act
            bool passValid = myToken.Validate(password, userId, expirationDate.AddSeconds(-5));

            //assert
            Assert.IsTrue(passValid);
        }

        [TestMethod]
        public void GeneratePassword_MustBeValidForUserUntilExpiration()
        {
            //arranje
            var myToken = new MyToken();
            var userId = "Leo";
            var expirationDate = DateTime.Now;
            var password = myToken.Generate(userId, expirationDate);

            //act
            bool passValid = myToken.Validate(password, userId, expirationDate);

            //assert
            Assert.IsTrue(passValid);
        }

        [TestMethod]
        public void GeneratePassword_MustBeInvalidAfterExpiration()
        {
            //arranje
            var myToken = new MyToken();
            var userId = "Leo";
            var expiration = DateTime.Now;
            var password = myToken.Generate(userId, expiration);

            //act
            bool passValid = myToken.Validate(password, userId, expiration.AddSeconds(1));

            //assert
            Assert.IsFalse(passValid);
        }
    }
}
