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
        public void SerializeATest()
        {
            A a1 = new A("A", 1.1f, new DateTime(2019, 12, 1), null);
            B b1 = new B("B", 3.65f, new DateTime(2019, 10, 1), null);
            C c1 = new C("C", 5.37f, new DateTime(2020, 1, 2), null);
            a1.ObjectB = b1;
            b1.ObjectC = c1;
            c1.ObjectA = a1;
            JSONSerialization<A> json = new JSONSerialization<A>("ObiektA.json", a1);
            json.serialize();
            string result = File.ReadAllText("ObiektA.json");
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
        public void SerializeBTest()
        {
            A a1 = new A("A", 1.1f, new DateTime(2019, 12, 1), null);
            B b1 = new B("B", 3.65f, new DateTime(2019, 10, 1), null);
            C c1 = new C("C", 5.37f, new DateTime(2020, 1, 2), null);
            a1.ObjectB = b1;
            b1.ObjectC = c1;
            c1.ObjectA = a1;
            JSONSerialization<B> json = new JSONSerialization<B>("ObiektB.json", b1);
            json.serialize();
            string result = File.ReadAllText("ObiektB.json");
            Assert.AreEqual(@"{
  ""$id"": ""1"",
  ""$type"": ""SerializationLibrary.B, SerializationLibrary"",
  ""Name"": ""B"",
  ""Number"": 3.65,
  ""Date"": ""2019-10-01T00:00:00"",
  ""ObjectC"": {
    ""$id"": ""2"",
    ""$type"": ""SerializationLibrary.C, SerializationLibrary"",
    ""Name"": ""C"",
    ""Number"": 5.37,
    ""Date"": ""2020-01-02T00:00:00"",
    ""ObjectA"": {
      ""$id"": ""3"",
      ""$type"": ""SerializationLibrary.A, SerializationLibrary"",
      ""Name"": ""A"",
      ""Number"": 1.1,
      ""Date"": ""2019-12-01T00:00:00"",
      ""ObjectB"": {
        ""$ref"": ""1""
      }
    }
  }
}", result);
        }
        
        [TestMethod]
        public void SerializeCTest()
        {
          A a1 = new A("A", 1.1f, new DateTime(2019, 12, 1), null);
          B b1 = new B("B", 3.65f, new DateTime(2019, 10, 1), null);
          C c1 = new C("C", 5.37f, new DateTime(2020, 1, 2), null);
          a1.ObjectB = b1;
          b1.ObjectC = c1;
          c1.ObjectA = a1;
          JSONSerialization<C> json = new JSONSerialization<C>("ObiektC.json", c1);
          json.serialize();
          string result = File.ReadAllText("ObiektC.json");
          Assert.AreEqual(@"{
  ""$id"": ""1"",
  ""$type"": ""SerializationLibrary.C, SerializationLibrary"",
  ""Name"": ""C"",
  ""Number"": 5.37,
  ""Date"": ""2020-01-02T00:00:00"",
  ""ObjectA"": {
    ""$id"": ""2"",
    ""$type"": ""SerializationLibrary.A, SerializationLibrary"",
    ""Name"": ""A"",
    ""Number"": 1.1,
    ""Date"": ""2019-12-01T00:00:00"",
    ""ObjectB"": {
      ""$id"": ""3"",
      ""$type"": ""SerializationLibrary.B, SerializationLibrary"",
      ""Name"": ""B"",
      ""Number"": 3.65,
      ""Date"": ""2019-10-01T00:00:00"",
      ""ObjectC"": {
        ""$ref"": ""1""
      }
    }
  }
}", result);
        }

        [TestMethod]
        public void DeserializeATest()
        {
            A a1 = new A("A", 1.1f, new DateTime(2019, 12, 1), null);
            B b1 = new B("B", 3.65f, new DateTime(2019, 10, 1), null);
            C c1 = new C("C", 5.37f, new DateTime(2020, 1, 2), null);
            a1.ObjectB = b1;
            b1.ObjectC = c1;
            c1.ObjectA = a1;
            JSONSerialization<A> json = new JSONSerialization<A>("ObiektA.json", a1);
            A a2 = json.deserialize();
            Assert.AreEqual(a1.Name, a2.Name);
            Assert.AreEqual(a1.Number, a2.Number);
            Assert.AreEqual(a1.Date, a2.Date);
            Assert.AreEqual(a1.ObjectB.Name, a2.ObjectB.Name);
        }
        
        [TestMethod]
        public void DeserializeBTest()
        {
          A a1 = new A("A", 1.1f, new DateTime(2019, 12, 1), null);
          B b1 = new B("B", 3.65f, new DateTime(2019, 10, 1), null);
          C c1 = new C("C", 5.37f, new DateTime(2020, 1, 2), null);
          a1.ObjectB = b1;
          b1.ObjectC = c1;
          c1.ObjectA = a1;
          JSONSerialization<B> json = new JSONSerialization<B>("ObiektB.json", b1);
          B b2 = json.deserialize();
          Assert.AreEqual(b1.Name, b2.Name);
          Assert.AreEqual(b1.Number, b2.Number);
          Assert.AreEqual(b1.Date, b2.Date);
          Assert.AreEqual(b1.ObjectC.Name, b2.ObjectC.Name);
        }
        
        [TestMethod]
        public void DeserializeCTest()
        {
            A a1 = new A("A", 1.1f, new DateTime(2019, 12, 1), null);
            B b1 = new B("B", 3.65f, new DateTime(2019, 10, 1), null);
            C c1 = new C("C", 5.37f, new DateTime(2020, 1, 2), null);
            a1.ObjectB = b1;
            b1.ObjectC = c1;
            c1.ObjectA = a1;
            JSONSerialization<C> json = new JSONSerialization<C>("ObiektC.json", c1);
            C c2 = json.deserialize();
            Assert.AreEqual(c1.Name, c2.Name);
            Assert.AreEqual(c1.Number, c2.Number);
            Assert.AreEqual(c1.Date, c2.Date);
            Assert.AreEqual(c1.ObjectA.Name, c2.ObjectA.Name);
        }
    }
}
