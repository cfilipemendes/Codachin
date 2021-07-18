using Codachin.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Codachin.Test.Services
{

    [TestClass]
    public class GitApiServiceTest
    {
        IGitService service;
        IMock<IUrlValidator> validatorMock;


        [TestInitialize]
        public void Setup()
        {
            validatorMock = new Mock<IUrlValidator>();
            service = new GitApiService(validatorMock.Object);
            
        }

        [TestMethod]
        public void TestInitService_InvalidUrl()
        {

        }
    }
}
