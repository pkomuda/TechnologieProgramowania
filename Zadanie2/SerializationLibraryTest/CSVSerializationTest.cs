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
            CSVSerialization<A> csv = new CSVSerialization<A>("pliczek.csv",a);
            csv.serialize();
            string result = File.ReadAllText("pliczek.csv");
            Assert.AreEqual(";;1;Name;System.String;A;Number;1.10;Date;12/01/2019;ObjectB;SerializationLibrary.B;2" + "\n" +
                ";;2;Name;System.String;B;Number;3.65;Date;10/01/2019;ObjectC;SerializationLibrary.C;3" + "\n" +
                ";;3;Name;System.String;C;Number;5.37;Date;01/02/2020;ObjectA;SerializationLibrary.A;1" + "\n", result);
        }
    }
}

