using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerializationLibrary;
using System;
using System.IO;

namespace SerializationLibraryTest
{
    [TestClass]
    public class CSVSerializationTest
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
            CSVSerialization<A> csv = new CSVSerialization<A>("ObiektA.csv", a1);
            csv.serialize();
            string result = File.ReadAllText("ObiektA.csv");
            Assert.AreEqual(
                "SerializationLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;SerializationLibrary.A;1;" + "\n" +
                "System.String|Name|A;System.Single|Number|1.1;System.DateTime|Date|12/01/2019;SerializationLibrary.B|ObjectB|ref2;" + "\n" +
                "SerializationLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;SerializationLibrary.B;2;" + "\n" +
                "System.String|Name|B;System.Single|Number|3.65;System.DateTime|Date|10/01/2019;SerializationLibrary.C|ObjectC|ref3;" + "\n" +
                "SerializationLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;SerializationLibrary.C;3;" + "\n" +
                "System.String|Name|C;System.Single|Number|5.37;System.DateTime|Date|01/02/2020;SerializationLibrary.A|ObjectA|ref1;" + "\n", result);
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
            CSVSerialization<B> csv = new CSVSerialization<B>("ObiektB.csv", b1);
            csv.serialize();
            string result = File.ReadAllText("ObiektB.csv");
            Assert.AreEqual(
                "SerializationLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;SerializationLibrary.B;1;" + "\n" +
                "System.String|Name|B;System.Single|Number|3.65;System.DateTime|Date|10/01/2019;SerializationLibrary.C|ObjectC|ref2;" + "\n" +
                "SerializationLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;SerializationLibrary.C;2;" + "\n" +
                "System.String|Name|C;System.Single|Number|5.37;System.DateTime|Date|01/02/2020;SerializationLibrary.A|ObjectA|ref3;" + "\n" +
                "SerializationLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;SerializationLibrary.A;3;" + "\n" +
                "System.String|Name|A;System.Single|Number|1.1;System.DateTime|Date|12/01/2019;SerializationLibrary.B|ObjectB|ref1;" + "\n", result);
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
            CSVSerialization<C> csv = new CSVSerialization<C>("ObiektC.csv", c1);
            csv.serialize();
            string result = File.ReadAllText("ObiektC.csv");
            Assert.AreEqual(
                "SerializationLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;SerializationLibrary.C;1;" + "\n" +
                "System.String|Name|C;System.Single|Number|5.37;System.DateTime|Date|01/02/2020;SerializationLibrary.A|ObjectA|ref2;" + "\n" +
                "SerializationLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;SerializationLibrary.A;2;" + "\n" +
                "System.String|Name|A;System.Single|Number|1.1;System.DateTime|Date|12/01/2019;SerializationLibrary.B|ObjectB|ref3;" + "\n" +
                "SerializationLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;SerializationLibrary.B;3;" + "\n" +
                "System.String|Name|B;System.Single|Number|3.65;System.DateTime|Date|10/01/2019;SerializationLibrary.C|ObjectC|ref1;" + "\n", result);
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
            CSVSerialization<A> csv = new CSVSerialization<A>("ObiektA.csv", a1);
            A a2 = csv.deserialize();
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
            CSVSerialization<B> csv = new CSVSerialization<B>("ObiektB.csv", b1);
            B b2 = csv.deserialize();
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
            CSVSerialization<C> csv = new CSVSerialization<C>("ObiektC.csv", c1);
            C c2 = csv.deserialize();
            Assert.AreEqual(c1.Name, c2.Name);
            Assert.AreEqual(c1.Number, c2.Number);
            Assert.AreEqual(c1.Date, c2.Date);
            Assert.AreEqual(c1.ObjectA.Name, c2.ObjectA.Name);
        }
    }
}
