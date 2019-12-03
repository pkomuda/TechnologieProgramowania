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
        public void SerializeTest()
        {
            A a = new A("A", 1.1f, new DateTime(2019, 12, 1), null);
            B b = new B("B", 3.65f, new DateTime(2019, 10, 1), null);
            C c = new C("C", 5.37f, new DateTime(2020, 1, 2), null);
            a.ObjectB = b;
            b.ObjectC = c;
            c.ObjectA = a;
            CSVSerialization<A> csv = new CSVSerialization<A>("pliczek.csv", a);
            csv.serialize();
            string result = File.ReadAllText("pliczek.csv");
            Assert.AreEqual(
                "SerializationLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;SerializationLibrary.A;1;" + "\n" +
                "System.String|Name|A;System.Single|Number|1.1;System.DateTime|Date|12/01/2019;SerializationLibrary.B|ObjectB|ref2;" + "\n" +
                "SerializationLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;SerializationLibrary.B;2;" + "\n" +
                "System.String|Name|B;System.Single|Number|3.65;System.DateTime|Date|10/01/2019;SerializationLibrary.C|ObjectC|ref3;" + "\n" +
                "SerializationLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;SerializationLibrary.C;3;" + "\n" +
                "System.String|Name|C;System.Single|Number|5.37;System.DateTime|Date|01/02/2020;SerializationLibrary.A|ObjectA|ref1;" + "\n", result);
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
            CSVSerialization<A> csv = new CSVSerialization<A>("pliczek.csv", a);
            A a2 = csv.deserialize();
            Console.WriteLine(a2);
            Console.WriteLine(a2.ObjectB);
            Console.WriteLine(a2.ObjectB.ObjectC);
            Console.WriteLine(a2.ObjectB.ObjectC.ObjectA);
        }
    }
}
