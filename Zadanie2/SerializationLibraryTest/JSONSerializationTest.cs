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
            JSONSerialization<A> json = new JSONSerialization<A>("plik.json", a);
            json.serialize();
            string result = File.ReadAllText("plik.json");
            Assert.AreEqual(@"{
  ""$id"": ""1"",
  ""$type"": ""SerializationLibrary.A, SerializationLibrary"",
  ""Name"": ""A"",
  ""Number"": 1.1,
  ""Date"": ""2019-12-01T00:00:00"",
  ""ObjectB"": {
    ""$id"": ""2"",
    ""$type"": ""SerializationLibrary.B, SerializationLibrary"",
    ""Name"": ""B"",
    ""Number"": 3.65,
    ""Date"": ""2019-10-01T00:00:00"",
    ""ObjectC"": {
      ""$id"": ""3"",
      ""$type"": ""SerializationLibrary.C, SerializationLibrary"",
      ""Name"": ""C"",
      ""Number"": 5.37,
      ""Date"": ""2020-01-02T00:00:00"",
      ""ObjectA"": {
        ""$ref"": ""1""
      }
    }
  }
}", result);
        }

        [TestMethod]
        public void DeserializeTest()
        {
            A a = new A("A", 1.1f, new DateTime(2019, 12, 1), null);
            B b = new B("B", 3.65f, new DateTime(2019, 10, 1), null);
            C c = new C("C", 5.37f, new DateTime(2020, 1, 2), null);
            a.ObjectB = b;
            b.ObjectC = c;
            c.ObjectA = a;
            JSONSerialization<A> json = new JSONSerialization<A>("plik.json", a);
            A a2 = json.deserialize();
            Console.WriteLine(a2);
            Console.WriteLine(a2.ObjectB);
            Console.WriteLine(a2.ObjectB.ObjectC);
            Console.WriteLine(a2.ObjectB.ObjectC.ObjectA);
        }
    }
}