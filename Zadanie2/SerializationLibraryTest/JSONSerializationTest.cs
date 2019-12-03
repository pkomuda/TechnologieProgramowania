using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerializationLibrary;

namespace SerializationLibraryTest
{
    [TestClass]
    public class JSONSerializationTest
    {
        [TestMethod]
        public void SerializeTest()
        {
            A a = new A("A", 1.1f, new DateTime(2019, 12, 1), null);
            B b = new B("B", 3.65f, new DateTime(2019, 10, 1), null);
            C c = new C("C", 5.37f, new DateTime(2020, 1, 2), null);
            a.ObjectB = b;
            b.ObjectC = c;
            c.ObjectA = a;
            JSONSerialization<A> csv = new JSONSerialization<A>("plik.json", a);
            csv.serialize();
            string result = File.ReadAllText("plik.json");
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void DeserializeTest()
        {
            
        }
    }
}