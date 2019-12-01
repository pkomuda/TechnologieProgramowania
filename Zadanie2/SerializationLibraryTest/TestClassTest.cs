using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerializationLibrary;

namespace SerializationLibraryTest
{
    [TestClass]
    public class TestClassTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            TestClass test = new TestClass();
            Assert.AreEqual(test.testIt(), "Hello!");
        }
    }
}

