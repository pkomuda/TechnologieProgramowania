using Library;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            DataRepository dataRepository = new DataRepository();
            dataRepository.FillData = new RandomFill();
            foreach(Client c in dataRepository.GetAllClients())
            {
                System.Console.WriteLine(c.ToString());
            }
            System.Console.WriteLine("/n");
            foreach (Catalog c in dataRepository.GetAllCatalogs())
            {
                System.Console.WriteLine(c.ToString());
            }
            System.Console.WriteLine("/n");
            foreach (Inventory i in dataRepository.GetAllInventories())
            {
                System.Console.WriteLine(i.ToString());
            }
            System.Console.WriteLine("/n");
            foreach (Event e  in dataRepository.GetAllEvents())
            {
                System.Console.WriteLine(e.ToString());
            }
            System.Console.WriteLine("/n");
            /*
            System.Console.WriteLine(dataRepository.GetAllEvents().ToString());
            Catalog ca = new Catalog("Ślepnąc od świateł", "Jakub Żulczyk");
            Inventory i = new Inventory(ca, 99);
            Client c = new Client("Przemek", "Komuda");
            foreach(Event ev in dataRepository.GetAllEvents())
            {
                Console.WriteLine(ev.ToString());
            }
            dataRepository.AddEvent(e);
            Console.WriteLine();
            foreach (Event ev in dataRepository.GetAllEvents())
            {
                Console.WriteLine(ev.ToString());
            }
            dataRepository.UpdateEvent("test", e);
            Console.WriteLine();
            foreach (Event ev in dataRepository.GetAllEvents())
            {
                Console.WriteLine(ev.ToString());
            }*/
            Console.ReadKey();
        }
    }
}
