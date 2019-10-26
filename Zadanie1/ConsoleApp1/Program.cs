using Library;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*DataRepository dataRepository = new DataRepository();
            dataRepository.FillData = new ConstansFill();
            System.Console.WriteLine(dataRepository.GetAllEvents().ToString());
            Catalog ca = new Catalog("Ślepnąc od świateł", "Jakub Żulczyk");
            Inventory i = new Inventory(ca, 99);
            Client c = new Client("Przemek", "Komuda");
            Event e = new Event(c, i, DateTime.Now, DateTime.Now.AddDays(1));

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
