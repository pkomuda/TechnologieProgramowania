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
                        DataContext dataContext = new DataContext();
                        DataFill graphFill = new GraphFill();
                        graphFill.Fill(dataContext);
                        JSON json = new JSON();
                        json.Serialize(dataContext.Clients, "clients.json");
                        json.Serialize(dataContext.Books, "books.json");
                        json.Serialize(dataContext.Events, "events.json");
                        json.Serialize(dataContext.Inventories, "inventories.json");
                        json.Serialize(dataContext.Notifications, "notifications.json");
                        Console.WriteLine("Serialization successful\n" +
                                          "Press any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    case '2':
                    {
                        Console.Clear();
                        JSON json = new JSON();
                        DataContext temp = json.Deserialize("clients.json", "books.json", "events.json", "inventories.json", "notifications.json");
                        DataService dataService = new DataService(new DataRepository(temp));
                        Console.WriteLine(dataService);
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
                        Console.WriteLine("Serialization successful\n" +
                                          "Press any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    case '4':
                    {
                        Console.Clear();
                        CSV csv = new CSV();
                        DataContext temp = csv.Deserialize("clients.csv", "books.csv", "events.csv", "inventories.csv", "notifications.csv");
                        DataService dataService = new DataService(new DataRepository(temp));
                        Console.WriteLine(dataService);
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
