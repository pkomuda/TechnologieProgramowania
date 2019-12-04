using System;
using System.IO;
using SerializationLibrary;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("1. Serialize to JSON\n" +
                                  "2. Deserialize from JSON\n" +
                                  "3. Serialize to CSV\n" +
                                  "4. Deserialize from CSV\n" +
                                  "0. Exit\n\n");
                key = Console.ReadKey();
                switch (key.KeyChar)
                {
                    case '1':
                    {
                        Console.Clear();
                        A a1 = new A("A", 1.1f, new DateTime(2019, 12, 1), null);
                        B b1 = new B("B", 3.65f, new DateTime(2019, 10, 1), null);
                        C c1 = new C("C", 5.37f, new DateTime(2020, 1, 2), null);
                        a1.ObjectB = b1;
                        b1.ObjectC = c1;
                        c1.ObjectA = a1;
                        JSONSerialization<A> json = new JSONSerialization<A>("ObiektA.json", a1);
                        json.serialize();
                        Console.WriteLine("Serialization successful\n" +
                                          "Press any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    case '2':
                    {
                        Console.Clear();
                        A a2 = null;
                        JSONSerialization<A> json = null;
                        try
                        {
                            json = new JSONSerialization<A>("ObiektA.json", a2);
                            a2 = json.deserialize();
                        }
                        catch (FileNotFoundException e)
                        {
                            Console.WriteLine("Serialize the object first\n" +
                                              "Press any key to continue");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                        Console.WriteLine(a2);
                        Console.WriteLine("\nPress any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    case '3':
                    {
                        Console.Clear();
                        A a1 = new A("A", 1.1f, new DateTime(2019, 12, 1), null);
                        B b1 = new B("B", 3.65f, new DateTime(2019, 10, 1), null);
                        C c1 = new C("C", 5.37f, new DateTime(2020, 1, 2), null);
                        a1.ObjectB = b1;
                        b1.ObjectC = c1;
                        c1.ObjectA = a1;
                        CSVSerialization<B> csv = new CSVSerialization<B>("ObiektB.csv", b1);
                        csv.serialize();
                        Console.WriteLine("Serialization successful\n" +
                                          "Press any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    case '4':
                    {
                        Console.Clear();
                        B b2 = null;
                        CSVSerialization<B> csv = null;
                        try
                        {
                            csv = new CSVSerialization<B>("ObiektB.csv", b2);
                            b2 = csv.deserialize();
                        }
                        catch (FileNotFoundException e)
                        {
                            Console.WriteLine("Serialize the object first\n" +
                                              "Press any key to continue");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                        Console.WriteLine(b2);
                        Console.WriteLine("\nPress any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    case '0':
                    {
                        break;
                    }
                    default:
                    {
                        Console.Clear();
                        Console.WriteLine("No such option\n" +
                                          "Press any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                }
            }
            while (key.KeyChar != '0');
        }
    }
}
