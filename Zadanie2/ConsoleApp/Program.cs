using System;
using SerializationLibrary;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TestClass hello = new TestClass();
            Console.WriteLine(hello.testIt());
            Console.ReadKey();
        }
    }
}
