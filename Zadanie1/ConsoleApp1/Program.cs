using Library;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            DataRepository dataRepository = new DataRepository();
            dataRepository.fillData = new ConstansFill();
            dataRepository.Fill();
            Console.WriteLine("START");
            foreach (Client c in dataRepository.GetAllClients())
            {
                Console.WriteLine(c.FirstName);
            }
            Console.ReadKey();
        }
    }
}
