using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Codachin.Tests
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void TestRunMain_LogValid()
        {
            Assert.AreEqual(0, Startup.Main(new string[] { "log" }));
        }


        [TestMethod]
        public void TestRunMain_InvalidInput()
        {
            Assert.AreEqual(1, Startup.Main(new string[] { "Whatever"}));
        }

    }
}
