using Library;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            DataRepository dataRepository = new DataRepository();
            dataRepository.FillData = new ConstansFill();
            Console.WriteLine("START");
            foreach (Client c in dataRepository.GetAllClients())
            {
                Console.WriteLine(c.FirstName);
            }
            Console.ReadKey();
        }
    }
}
