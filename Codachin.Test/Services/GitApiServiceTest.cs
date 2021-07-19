using Codachin.Services;
using Codachin.Services.Exceptions;
using Codachin.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Codachin.Test.Services
{

    [TestClass]
    public class GitApiServiceTest
    {
        IGitService service;
        Mock<IUrlValidator> validatorMock;
        Mock<IHttpNetWrapper> httpWrapperMock;


        [TestInitialize]
        public void Setup()
        {
            validatorMock = new Mock<IUrlValidator>();
            httpWrapperMock = new Mock<IHttpNetWrapper>();
            service = new GitApiService(validatorMock.Object, httpWrapperMock.Object);
        }

        [TestMethod]
        public void TestApiService_NullUrl()
        {
            validatorMock.Setup(mock => mock.ValidateUrl(It.IsAny<string>())).Throws(new GitException());

            Assert.ThrowsException<GitException>( () => service.Init(null));
        }

        [TestMethod]
        public void TestApiService_MalformedUrl()
        {
            validatorMock.Setup(mock => mock.ValidateUrl(It.IsAny<string>())).Throws(new GitException());

            Assert.ThrowsException<GitException>(() => service.Init("batatinhas"));
        }
        [TestMethod]
        public void TestApiService_MalformedUrlWithDotGit()
        {
            validatorMock.Setup(mock => mock.ValidateUrl(It.IsAny<string>())).Throws(new GitException());

            Assert.ThrowsException<GitException>(() => service.Init("teste/teste.git"));
        }

        [TestMethod]
        public void TestApiService_ValidUrl()
        {
            validatorMock.Setup(mock => mock.ValidateUrl(It.IsAny<string>())).Returns(new Tuple<string,string>("user123","repo123"));

            var x = service.Init("https://github.com/user123/repo123.git");

            Assert.AreEqual("user123", x.GitUser);
            Assert.AreEqual("repo123", x.Repository);

        }

        [TestMethod]
        public void TestApiService_ValidGetLog()
        {
            validatorMock.Setup(mock => mock.ValidateUrl(It.IsAny<string>())).Returns(new Tuple<string, string>("user123", "repo123"));
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.OK;
            string json = @"[
            {
            ""sha"": ""a3e957ad83fe194db984b7c0524bd04db7469547"",
            ""commit"": {
                        ""author"": {
                            ""date"": ""06/22/2021 01:01:41""
                        },
                        ""message"":""Message Test""
            },
            ""author"": {
                            ""login"": ""testeLoginUser""
                        }
            }
            ]";
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            response.Content = httpContent;
            httpWrapperMock.Setup(mock => mock.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(response));

            var singleValue = service.Init("https://github.com/user123/repo123.git?page=1&per_page=3").GetLogAsync().Result;

            foreach (var item in singleValue)
            {
                Assert.AreEqual("a3e957ad83fe194db984b7c0524bd04db7469547", item.Sha);
                Assert.AreEqual("06/22/2021 01:01:41", item.Date);
                Assert.AreEqual("Message Test", item.Message);
                Assert.AreEqual("testeLoginUser", item.Author);
            }

        }
    }
}
