using Codachin.Services;
using Codachin.Services.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Codachin.Test
{
    [TestClass]
    public class UrlValidatorTest
    {
        private IUrlValidator urlValidator;

        [TestInitialize]
        public void Setup()
        {
            urlValidator = new GitUrlValidator();
        }


        [TestMethod]
        public void NullUrl()
        {
            Assert.ThrowsException<ArgumentNullException>(() => urlValidator.ValidateUrl(null));
        }

        [TestMethod]
        public void InvalidUrl()
        {
            Assert.ThrowsException<GitException>(() => urlValidator.ValidateUrl("www.sapo.pt"));
        }

        [TestMethod]
        public void ValidUrl()
        {
           var tupleUserAndRepo = urlValidator.ValidateUrl("https://github.com/user/repo.git");
            Assert.AreEqual("user", tupleUserAndRepo.Item1);
            Assert.AreEqual("repo", tupleUserAndRepo.Item2);

        }


    }

}
