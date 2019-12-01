using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerializationLibrary;
using System;

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
            Console.WriteLine("Inicjalizacje");
            CSVSerialization<A> csv = new CSVSerialization<A>("pliczek.csv",a);
            Console.WriteLine("Inicjalizacja CSV");
            csv.serialize();
            Console.WriteLine("Serializacja");
        }
    }
}

