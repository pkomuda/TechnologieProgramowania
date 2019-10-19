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
            dataRepository.fillData.Fill(new DataContext());
            foreach (Client c in dataRepository.GetAllClients())
            {
                Console.WriteLine(c.FirstName);
            }
            Console.ReadKey();
        }
    }
}
