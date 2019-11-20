using System;
using Library;

namespace SerializationApp
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
                        DataRepository dataRepository = new DataRepository();
                        dataRepository.FillData = new GraphFill();
                        JSON json = new JSON();
                        json.Serialize(dataRepository.GetClientList(), "clients.json");
                        json.Serialize(dataRepository.GetCatalogDictionary(), "books.json");
                        json.Serialize(dataRepository.GetEventCollection(), "events.json");
                        json.Serialize(dataRepository.GetInventoryList(), "inventories.json");
                        json.Serialize(dataRepository.GetNotifications(), "notifications.json");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    case '2':
                    {
                        Console.Clear();
                        JSON json = new JSON();
                        DataContext temp = json.Deserialize("clients.json", "books.json", "events.json", "inventories.json", "notifications.json");
                        DataRepository dataRepository = new DataRepository(temp);
                        Console.WriteLine(dataRepository);
                        Console.WriteLine("\nPress any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    case '3':
                    {
                        Console.Clear();
                        DataRepository dataRepository = new DataRepository();
                        dataRepository.FillData = new GraphFill();
                        CSV csv = new CSV();
                        csv.Serialize(dataRepository.GetClientList(), "clients.csv");
                        csv.Serialize(dataRepository.GetCatalogDictionary(), "books.csv");
                        csv.Serialize(dataRepository.GetEventCollection(), "events.csv");
                        csv.Serialize(dataRepository.GetInventoryList(), "inventories.csv");
                        csv.Serialize(dataRepository.GetNotifications(), "notifications.csv");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    case '4':
                    {
                        Console.Clear();
                        CSV csv = new CSV();
                        DataContext temp = csv.Deserialize("clients.csv", "books.csv", "events.csv", "inventories.csv", "notifications.csv");
                        DataRepository dataRepository = new DataRepository(temp);
                        Console.WriteLine(dataRepository);
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
