using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Codachin.Tests
{
    [TestClass]
    public class StartupTest
    {

        [TestMethod]
        public void TestRunMain_NoInput()
        {
            Assert.AreEqual(-1, Startup.Main(new string[] {}).Result);
        }

        [TestMethod]
        public void TestRunMain_InvalidInputInvalidUrl()
        {
            Assert.AreEqual(-2, Startup.Main(new string[] { "Whatever" }).Result);
        }

        [TestMethod]
        public void TestRunMain_NonExistentOption()
        {
            Assert.AreEqual(-2, Startup.Main(new string[] { "https://github.com/microsoft/vscode.git -XYZ 1" }).Result);
        }

        [TestMethod]
        public void TestRunMain_Valid()
        {
            var result = Startup.Main(new string[] { "https://github.com/cfilipemendes/nossacoin.git" }).Result;
            Console.WriteLine(Directory.GetCurrentDirectory());

            string[] allfiles = Directory.GetFiles(Environment.CurrentDirectory, "*.*", SearchOption.AllDirectories);

            foreach (var item in allfiles)
            {
                Console.WriteLine(item);
            }

            //We should delete the created folder here.
             
            Assert.AreEqual(0, result);

        }
    }
}
